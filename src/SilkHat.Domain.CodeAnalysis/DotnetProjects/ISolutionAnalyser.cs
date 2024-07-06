using System.Collections.Immutable;
using SilkHat.Domain.CodeAnalysis.DotnetProjects.Models;

namespace SilkHat.Domain.CodeAnalysis.DotnetProjects
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
        

        Task LoadSolution();
        Task BuildSolution();
    }
}