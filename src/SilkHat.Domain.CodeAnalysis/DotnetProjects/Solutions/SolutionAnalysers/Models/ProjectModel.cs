namespace SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions.SolutionAnalysers.Models
{
    public record ProjectModel(string Name, string Path, string AssemblyName, LanguageType LanguageType, SolutionModel SolutionModel);
}