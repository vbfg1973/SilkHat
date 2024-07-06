using SilkHat.Domain.CodeAnalysis.DotnetProjects.Models;

namespace SilkHat.Domain.CodeAnalysis.DotnetProjects
{
    public interface ISolutionAnalyzer
    {
        List<SolutionAnalyserBuildResult> BuildResults { get; }
        bool IsLoaded { get; }
        SolutionModel Solution { get; init; }
        List<ProjectModel> Projects { get; init; }
    }
}