namespace SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions.SolutionAnalysers.Models
{
    public record DocumentModel(
        string Name,
        string Path,
        string RelativePath,
        ProjectModel ProjectModel,
        LanguageType LanguageType);

    public record EnhancedDocumentModel(
        string Name,
        string Path,
        string RelativePath,
        string SourceText,
        ProjectModel ProjectModel,
        LanguageType LanguageType);
}