using SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions.SolutionAnalysers.Models;

namespace SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions.SolutionAnalysers.ProjectStructure
{
    public record ProjectStructureModel(
        string Name,
        string FullPath,
        string RelativePath,
        List<ProjectStructureModel> Children,
        ProjectStructureType ProjectStructureType,
        ProjectModel ProjectModel);
}