using System.Collections.Concurrent;
using System.Diagnostics;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.CodeAnalysis.Text;
using Microsoft.Extensions.Logging;
using SilkHat.Domain.CodeAnalysis.Abstract;
using SilkHat.Domain.CodeAnalysis.Analysis.FileSystem;
using SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions.SolutionAnalysers.Models;
using SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions.SolutionAnalysers.ProjectStructure;
using SilkHat.Domain.CodeAnalysis.Walkers.CSharp;
using SilkHat.Domain.Common;
using SilkHat.Domain.Graph.TripleDefinitions.Nodes;
using SilkHat.Domain.Graph.TripleDefinitions.Triples;
using SilkHat.Domain.Graph.TripleDefinitions.Triples.Abstract;

namespace SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions.SolutionAnalysers
{
    public interface ISolutionAnalyser
    {
        List<SolutionAnalyserBuildResult> BuildResults { get; }
        bool IsLoaded { get; }
        bool IsBuilt { get; }
        bool HasWarnings { get; }
        bool HasFailures { get; }
        SolutionModel Solution { get; }
        List<ProjectModel> Projects { get; }
        ConcurrentBag<Triple> Triples { get; }
        
        List<DocumentModel> ProjectDocuments(ProjectModel projectModel);

        Task<ProjectStructureModel> ProjectStructure(ProjectModel projectModel);
        Task<EnhancedDocumentModel> EnhancedDocumentModel(ProjectModel projectModel, string fullPath);
        Task<DocumentModel> DocumentModel(ProjectModel projectModel, string fullPath);

        Task CodeAnalysis(ProjectModel projectModel);

        Task LoadSolution();
        Task BuildSolution();
    }

    public class SolutionAnalyser : ISolutionAnalyser
    {
        private readonly ILogger<SolutionAnalyser> _logger;
        private readonly IProjectStructureBuilder _projectStructureBuilder;
        private readonly SolutionAnalyserOptions _solutionAnalyserOptions;
        private readonly ConcurrentDictionary<string, Compilation> _compilations = new();
        private List<Project> _projects;
        private Solution _solution;

        public SolutionAnalyser(SolutionAnalyserOptions solutionAnalyserOptions,
            IProjectStructureBuilder projectStructureBuilder, ILogger<SolutionAnalyser> logger)
        {
            _solutionAnalyserOptions = solutionAnalyserOptions;
            _projectStructureBuilder = projectStructureBuilder;
            _logger = logger;
        }

        public List<DocumentModel> ProjectDocuments(ProjectModel projectModel)
        {
            Project? project = GetProject(projectModel);
            return project != null ? MapDocumentModels(project).ToList() : [];
        }

        public async Task<EnhancedDocumentModel> EnhancedDocumentModel(ProjectModel projectModel, string fullPath)
        {
            Project? project = GetProject(projectModel);

            Document? document = project!.Documents.SingleOrDefault(x => x.FilePath == fullPath);

            return await MapEnhancedDocumentModel(projectModel, document!);
        }

        public async Task<DocumentModel> DocumentModel(ProjectModel projectModel, string fullPath)
        {
            Project? project = GetProject(projectModel);

            Document? document = project!.Documents.SingleOrDefault(x => x.FilePath == fullPath);

            return await MapDocumentModel(projectModel, document!);
        }

        public async Task<ProjectStructureModel> ProjectStructure(ProjectModel projectModel)
        {
            return await _projectStructureBuilder.ProjectStructure(projectModel, ProjectDocuments(projectModel));
        }

        private Project? GetProject(ProjectModel projectModel)
        {
            return _solution.Projects.FirstOrDefault(x => x.AssemblyName == projectModel.AssemblyName);
        }

        #region Analysis

        public async Task CodeAnalysis(ProjectModel projectModel)
        {
            Project? project = GetProject(projectModel);

            _logger.LogTrace("{Method} {ProjectName}", nameof(CodeAnalysis), project!.Name);

            await Console.Error.WriteLineAsync($"Code analysis: {project!.Name}");
            Compilation? compilation = await project.GetCompilationAsync();

            if (compilation == null) return;

            IEnumerable<SyntaxTree> syntaxTrees =
                compilation
                    .SyntaxTrees
                    .Where(x => !x.FilePath.Contains("obj"));

            FileSystemAnalyzer fileSystemAnalyzer = new();
            foreach (SyntaxTree syntaxTree in syntaxTrees)
            {
                IList<Triple> fileSystemTriples = await fileSystemAnalyzer.GetFileSystemChain(syntaxTree.FilePath);
                FileNode fileNode = fileSystemTriples
                    .OfType<TripleIncludedIn>()
                    .Where(x => x.NodeA is FileNode)
                    .Select(x => x.NodeA as FileNode)
                    .First()!;

                foreach (Triple triple in fileSystemTriples.Distinct())
                {
                    Triples.Add(triple);
                }

                SemanticModel semanticModel = compilation.GetSemanticModel(syntaxTree);

                WalkerOptions walkerOptions = new(new DotnetOptions(syntaxTree, semanticModel, project), true);
                CSharpTypeDiscoveryWalker walker = new(fileNode!, new ProjectNode(project.Name), walkerOptions);

                foreach (Triple triple in walker.Walk().Distinct())
                {
                    Triples.Add(triple);
                }
            }
        }

        #endregion


        #region Properties

        public List<SolutionAnalyserBuildResult> BuildResults { get; } = new();
        public bool IsLoaded { get; set; }
        public bool IsBuilt { get; private set; }
        public bool HasFailures => BuildResults.Any(x => x.DiagnosticKind == WorkspaceDiagnosticKind.Failure);
        public bool HasWarnings => BuildResults.Any(x => x.DiagnosticKind == WorkspaceDiagnosticKind.Warning);
        public SolutionModel Solution { get; private set; }
        public List<ProjectModel> Projects { get; private set; }

        public ConcurrentBag<Triple> Triples { get; } = new();

        #endregion

        #region Map to public models

        private SolutionModel MapSolutionModel()
        {
            return new SolutionModel(
                PathUtilities.RemoveExtension(Path.GetFileName(_solution.FilePath!)),
                _solution.FilePath!);
        }

        private ProjectModel MapProjectModel(Project project)
        {
            return new ProjectModel(
                project.Name,
                project.FilePath!,
                project.AssemblyName,
                MapLanguage(project),
                MapSolutionModel()
            );
        }

        private async Task<DocumentModel> MapDocumentModel(ProjectModel projectModel, Document document)
        {
            return new DocumentModel(
                document.Name,
                document.FilePath!,
                document.FilePath!.Replace(GetCommonDocumentRoot(projectModel), ""),
                projectModel,
                projectModel.LanguageType);
        }

        private async Task<EnhancedDocumentModel> MapEnhancedDocumentModel(ProjectModel projectModel, Document document)
        {
            document.TryGetText(out SourceText? sourceTextObject);
            string sourceText = sourceTextObject!.ToString();

            return new EnhancedDocumentModel(
                document.Name,
                document.FilePath!,
                document.FilePath!.Replace(GetCommonDocumentRoot(projectModel), ""),
                sourceText,
                projectModel,
                projectModel.LanguageType);
        }

        private string GetCommonDocumentRoot(ProjectModel projectModel)
        {
            Project? project = GetProject(projectModel);
            List<string> filePaths = project!.Documents.Select(x => x.FilePath!).ToList();
            return PathUtilities.CommonParent(filePaths);
        }

        private IEnumerable<DocumentModel> MapDocumentModels(Project project)
        {
            List<string?> paths = project
                .Documents
                .Where(doc => !doc.FilePath!.Contains("bin"))
                .Where(doc => !doc.FilePath!.Contains("obj"))
                .Where(doc => !doc.FilePath!.Contains(".nuget"))
                .Select(x => x.FilePath)
                .ToList();
            paths.Add(project.FilePath);
            string commonParent = PathUtilities.CommonParent(paths);

            foreach (Document document in project.Documents)
            {
                yield return new DocumentModel(
                    document.Name,
                    document.FilePath!,
                    document.FilePath!.Replace(commonParent, ""),
                    MapProjectModel(project),
                    MapLanguage(project)
                );
            }
        }

        private LanguageType MapLanguage(Project project)
        {
            return project.Language switch
            {
                "C#" => LanguageType.CSharp,
                "VisualBasic" => LanguageType.VisualBasic,
                _ => LanguageType.NoneCode
            };
        }

        #endregion

        #region Load and Build

        public async Task LoadSolution()
        {
            _solution = await ReadAndLoadSolution(_solutionAnalyserOptions);

            IsLoaded = true;
            IsBuilt = false;

            _projects = _solution.Projects.ToList();

            // _compilations = IsLoaded ? BuildIt().Result : new Dictionary<string, Compilation>();

            Solution = MapSolutionModel();
            Projects = _solution.Projects.Select(MapProjectModel).ToList();
        }

        public async Task BuildSolution()
        {
            await BuildIt();
        }

        private async Task<Solution> ReadAndLoadSolution(SolutionAnalyserOptions solutionAnalyserOptions)
        {
            // Setup MSBuild
            if (!MSBuildLocator.IsRegistered) MSBuildLocator.RegisterDefaults();

            using MSBuildWorkspace workspace = MSBuildWorkspace.Create();
            workspace.WorkspaceFailed += (o, e) =>
            {
                Console.Error.WriteLine($"MSBuild {e.Diagnostic.Kind} {e.Diagnostic.Message}");
                _logger.LogError("MSBuild {MSBuildDiagnosticKind} {MSBuildDiagnosticMessage}", e.Diagnostic.Kind,
                    e.Diagnostic.Message);

                BuildResults.Add(new SolutionAnalyserBuildResult(e.Diagnostic.Kind, e.Diagnostic.Message));
            };

            return await workspace.OpenSolutionAsync(solutionAnalyserOptions.SolutionPath);
        }

        private async Task BuildIt()
        {
            Stopwatch sw = new();
            sw.Start();

            ParallelOptions parallelOptions = new()
            {
                MaxDegreeOfParallelism = 20
            };

            await Parallel.ForEachAsync(_solution.Projects, parallelOptions, async (project, token) =>
            {
                await Console.Error.WriteLineAsync($"Building: {Solution.Name} - {project.Name}");
                Compilation? compilation = await project.GetCompilationAsync(token);

                if (compilation != null)
                {
                    _compilations.TryAdd(project.Name, compilation);
                    await Console.Error.WriteLineAsync($"Finished Building: {Solution.Name} - {project.Name}");
                }
            });

            sw.Stop();

            await Console.Error.WriteLineAsync($"Finished Building Solution {Solution.Name} - {sw.Elapsed}");
            IsBuilt = true;
        }

        #endregion
    }
}