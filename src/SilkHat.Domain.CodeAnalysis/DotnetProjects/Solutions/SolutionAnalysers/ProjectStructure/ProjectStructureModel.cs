namespace SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions.SolutionAnalysers.ProjectStructure
{
    public record ProjectStructureModel(
        string Name,
        string FilePath,
        List<ProjectStructureModel> Children,
        ProjectStructureType ProjectStructureType);
}