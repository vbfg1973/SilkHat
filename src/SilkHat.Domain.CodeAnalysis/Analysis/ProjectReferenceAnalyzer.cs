﻿using Buildalyzer;
using Microsoft.CodeAnalysis;
using SilkHat.Domain.CodeAnalysis.Analysis.FileSystem;
using SilkHat.Domain.Graph.SemanticTriples.Nodes;
using SilkHat.Domain.Graph.SemanticTriples.Triples;
using SilkHat.Domain.Graph.SemanticTriples.Triples.Abstract;

namespace SilkHat.Domain.CodeAnalysis.Analysis
{
    public class ProjectReferenceAnalyzer(
        Project project,
        IProjectAnalyzer projectAnalyzer,
        IAnalyzerResult analyzerResult)
        : IAnalyzer
    {
        private readonly IProjectAnalyzer _projectAnalyzer = projectAnalyzer;

        public async Task<IList<Triple>> Analyze()
        {
            List<Triple> list = new();

            string projectName = GetProjectNameFromPath(project.Name);
            FileSystemAnalyzer fileSystemAnalyzer = new();
            IList<Triple> fileSystemTriples = await fileSystemAnalyzer.GetFileSystemChain(project.FilePath!);

            FolderNode? folderNode = fileSystemTriples.Last().NodeB as FolderNode;
            ProjectNode projectNode = new(projectName);

            list.Add(new IncludedInTriple(projectNode, folderNode!));
            list.AddRange(GetPackageDependencies(projectNode));
            list.AddRange(GetProjectDependencies(projectNode));

            return list;
        }

        private IEnumerable<DependsOnProjectTriple> GetProjectDependencies(ProjectNode projectNode)
        {
            foreach (string analyzerResultProjectReference in analyzerResult.ProjectReferences)
            {
                ProjectNode node = new(GetProjectNameFromPath(analyzerResultProjectReference));
                yield return new DependsOnProjectTriple(projectNode, node);
            }
        }

        private IEnumerable<DependsOnPackageTriple> GetPackageDependencies(ProjectNode projectNode)
        {
            foreach (KeyValuePair<string, IReadOnlyDictionary<string, string>> x in analyzerResult.PackageReferences
                         .ToList())
            {
                string version = x.Value.Values.FirstOrDefault(x => x.Contains('.')) ?? "none";
                PackageNode packageNode = new(x.Key, x.Key, version);
                yield return new DependsOnPackageTriple(projectNode, packageNode);
            }
        }

        private static string GetProjectNameFromPath(string? projectPath)
        {
            if (projectPath == null) return string.Empty;

            string fileName = projectPath.Split(Path.DirectorySeparatorChar, StringSplitOptions.RemoveEmptyEntries)
                .Last();
            fileName = fileName.Replace(".csproj", "");
            fileName = fileName.Replace(".vbproj", "");

            return fileName;
        }
    }
}