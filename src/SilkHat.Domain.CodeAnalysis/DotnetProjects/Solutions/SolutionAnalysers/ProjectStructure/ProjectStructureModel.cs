namespace SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions.SolutionAnalysers.ProjectStructure
{
    public record ProjectStructureModel(
        string Name,
        List<ProjectStructureModel> Children,
        ProjectStructureType ProjectStructureType);
}