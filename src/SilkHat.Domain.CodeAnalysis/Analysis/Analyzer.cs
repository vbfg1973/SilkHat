// using System.Collections.Concurrent;
// using Buildalyzer;
// using Microsoft.CodeAnalysis;
// using Microsoft.Extensions.Logging;
// using SilkHat.Domain.CodeAnalysis.Abstract;
// using SilkHat.Domain.CodeAnalysis.Analysis.FileSystem;
// using SilkHat.Domain.CodeAnalysis.Walkers.CSharp;
// using SilkHat.Domain.Graph.TripleDefinitions.Nodes;
// using SilkHat.Domain.Graph.TripleDefinitions.Triples;
// using SilkHat.Domain.Graph.TripleDefinitions.Triples.Abstract;
//
// namespace SilkHat.Domain.CodeAnalysis.Analysis
// {
//     public class Analyzer : IAnalyzer
//     {
//         private readonly AnalysisConfig _analysisConfig;
//         private readonly AnalyzerManager _analyzerManager;
//         private readonly ILogger<Analyzer> _logger;
//         private readonly ILoggerFactory _loggerFactory;
//
//         private readonly ConcurrentBag<Triple> _triples = new();
//
//         public Analyzer(AnalysisConfig analysisConfig, ILoggerFactory loggerFactory)
//         {
//             _logger = loggerFactory.CreateLogger<Analyzer>();
//
//             _analysisConfig = analysisConfig;
//             _loggerFactory = loggerFactory;
//
//             _analyzerManager = new AnalyzerManager(_analysisConfig.Solution);
//         }
//
//         public async Task<IList<Triple>> Analyze()
//         {
//             _logger.LogTrace("{Method}", nameof(Analyze));
//
//             IEnumerable<IProjectAnalyzer> projectAnalyzers = _analyzerManager.Projects.Values;
//
//             AdhocWorkspace workspace = new();
//             List<(Project, IProjectAnalyzer, IAnalyzerResult)> projects = new();
//
//             ParallelOptions parallelOptions = new()
//             {
//                 MaxDegreeOfParallelism = 20
//             };
//
//             Parallel.ForEach(projectAnalyzers, parallelOptions,
//                 projectAnalyzer => { ProjectAnalysis(projectAnalyzer, workspace, projects).Wait(); });
//
//             await RelationshipStatistics();
//             // await ReportNamespaces();
//
//             return _triples.Distinct().ToList();
//         }
//
//         private async Task ProjectAnalysis(IProjectAnalyzer projectAnalyzer, AdhocWorkspace workspace,
//             List<(Project, IProjectAnalyzer, IAnalyzerResult)> projects)
//         {
//             _logger.LogTrace("{Method} {ProjectName}", nameof(ProjectAnalysis),
//                 projectAnalyzer.ProjectInSolution.ProjectName);
//
//             Project? project = projectAnalyzer.AddToWorkspace(workspace);
//             IAnalyzerResult? analyzerResult = projectAnalyzer.Build().First();
//             projects.Add((project, projectAnalyzer, analyzerResult));
//
//             await Console.Error.WriteLineAsync($"Project analysis: {project.Name}");
//
//             ProjectReferenceAnalyzer projectReferenceAnalyzer = new(project, projectAnalyzer, analyzerResult);
//
//
//             foreach (Triple triple in (await projectReferenceAnalyzer.Analyze()).Distinct())
//             {
//                 _triples.Add(triple);
//             }
//
//             await CodeAnalysis(project, projectAnalyzer, analyzerResult);
//
//             await Console.Error.WriteLineAsync($"Finished: {project.Name}");
//         }
//
//         private async Task CodeAnalysis(Project project, IProjectAnalyzer projectAnalyzer,
//             IAnalyzerResult analyzerResult)
//         {
//             _logger.LogTrace("{Method} {ProjectName}", nameof(CodeAnalysis), project.Name);
//
//             await Console.Error.WriteLineAsync($"Code analysis: {project.Name}");
//             Compilation? compilation = await project.GetCompilationAsync();
//
//             if (compilation == null) return;
//
//             IEnumerable<SyntaxTree> syntaxTrees =
//                 compilation
//                     .SyntaxTrees
//                     .Where(x => !x.FilePath.Contains("obj"));
//
//             FileSystemAnalyzer fileSystemAnalyzer = new();
//             foreach (SyntaxTree syntaxTree in syntaxTrees)
//             {
//                 IList<Triple> fileSystemTriples = await fileSystemAnalyzer.GetFileSystemChain(syntaxTree.FilePath);
//                 FileNode fileNode = fileSystemTriples
//                     .OfType<TripleIncludedIn>()
//                     .Where(x => x.NodeA is FileNode)
//                     .Select(x => x.NodeA as FileNode)
//                     .First()!;
//
//                 foreach (Triple triple in fileSystemTriples.Distinct())
//                 {
//                     _triples.Add(triple);
//                 }
//
//                 SemanticModel semanticModel = compilation.GetSemanticModel(syntaxTree);
//
//                 WalkerOptions walkerOptions = new(new DotnetOptions(syntaxTree, semanticModel, project), true);
//                 CSharpTypeDiscoveryWalker walker = new(fileNode!, new ProjectNode(project.Name), walkerOptions,
//                     _loggerFactory);
//
//                 foreach (Triple triple in walker.Walk().Distinct())
//                 {
//                     _triples.Add(triple);
//                 }
//             }
//         }
//
//         private async Task RelationshipStatistics()
//         {
//             Dictionary<string, List<Triple>> dictionary =
//                 _triples.GroupBy(x => x.Relationship.Type).ToDictionary(x => x.Key, x => x.ToList());
//
//             await Console.Error.WriteLineAsync();
//             foreach (KeyValuePair<string, List<Triple>> kvp in dictionary.OrderByDescending(x => x.Value.Count()))
//             {
//                 await Console.Error.WriteLineAsync($"{kvp.Key}: {kvp.Value.Count}");
//             }
//
//             await Console.Error.WriteLineAsync();
//         }
//
//         private async Task ReportNamespaces()
//         {
//             await Console.Error.WriteLineAsync();
//
//             IEnumerable<string> invokedNamespaces = _triples
//                 .OfType<TripleHasComplexity>()
//                 .Where(x => x.NodeB is MethodNode)
//                 .Select(x => x.NodeB.FullName)
//                 .Select(x => string.Join(".", x.Split('.').SkipLast(2)))
//                 .GroupBy(x => x)
//                 .Select(grouping => new { Namespace = grouping.Key, Count = grouping.Count() })
//                 .OrderBy(x => x.Namespace)
//                 .Select(x => $"ns: {x.Namespace} - {x.Count}");
//
//             await Console.Error.WriteLineAsync("Invoked namespaces:");
//             await Console.Error.WriteLineAsync("\t" + string.Join("\n\t", invokedNamespaces));
//             await Console.Error.WriteLineAsync();
//         }
//     }
// }