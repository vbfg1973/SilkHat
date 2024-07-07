﻿using System.Collections.Concurrent;
using System.Diagnostics;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.Extensions.Logging;
using SilkHat.Domain.CodeAnalysis.DotnetProjects.Models;
using SilkHat.Domain.Common;

namespace SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions.SolutionAnalysers
{
    public class SolutionAnalyser : ISolutionAnalyser
    {
        private readonly ILogger<SolutionAnalyser> _logger;
        private readonly SolutionAnalyserOptions _solutionAnalyserOptions;
        private ConcurrentDictionary<string, Compilation> _compilations;
        private List<Project> _projects;
        private Solution _solution;

        public SolutionAnalyser(SolutionAnalyserOptions solutionAnalyserOptions, ILogger<SolutionAnalyser> logger)
        {
            _solutionAnalyserOptions = solutionAnalyserOptions;
            _logger = logger;
        }

        #region Properties

        public List<SolutionAnalyserBuildResult> BuildResults { get; } = new();
        public bool IsLoaded { get; set; }
        public bool IsBuilt { get; private set; }
        public bool HasFailures => BuildResults.Any(x => x.DiagnosticKind == WorkspaceDiagnosticKind.Failure);
        public bool HasWarnings => BuildResults.Any(x => x.DiagnosticKind == WorkspaceDiagnosticKind.Warning);
        public SolutionModel Solution { get; private set; }
        public List<ProjectModel> Projects { get; private set; }

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
                project.AssemblyName
            );
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
            _compilations = await BuildIt();
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

        private async Task<ConcurrentDictionary<string, Compilation>> BuildIt()
        {
            Stopwatch sw = new();
            sw.Start();
            ConcurrentDictionary<string, Compilation> compilations = new();

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
                    compilations.TryAdd(project.Name, compilation);
                    await Console.Error.WriteLineAsync($"Finished Building: {Solution.Name} - {project.Name}");
                }
            });

            sw.Stop();

            await Console.Error.WriteLineAsync($"Finished Building Solution {Solution.Name} - {sw.Elapsed}");
            IsBuilt = true;
            return compilations;
        }

        #endregion
    }
}