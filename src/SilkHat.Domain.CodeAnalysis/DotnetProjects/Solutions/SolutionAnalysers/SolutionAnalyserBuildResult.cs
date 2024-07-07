using Microsoft.CodeAnalysis;

namespace SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions.SolutionAnalysers
{
    public record SolutionAnalyserBuildResult(WorkspaceDiagnosticKind DiagnosticKind, string Message);
}