using SilkHat.Domain.CodeAnalysis.DotnetProjects.Models;

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
        List<DocumentModel> ProjectDocuments(ProjectModel projectModel);
        

        Task LoadSolution();
        Task BuildSolution();
    }
}