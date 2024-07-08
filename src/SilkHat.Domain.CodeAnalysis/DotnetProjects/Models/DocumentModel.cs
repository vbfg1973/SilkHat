namespace SilkHat.Domain.CodeAnalysis.DotnetProjects.Models
{
    public record DocumentModel(string Name, string Path, string RelativePath, ProjectModel ProjectModel, LanguageType LanguageType);
}