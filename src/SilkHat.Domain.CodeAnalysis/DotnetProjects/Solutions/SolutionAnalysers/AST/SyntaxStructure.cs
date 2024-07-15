namespace SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions.SolutionAnalysers.AST
{
    public record SyntaxStructure(string Name, List<SyntaxStructure> Children);
}