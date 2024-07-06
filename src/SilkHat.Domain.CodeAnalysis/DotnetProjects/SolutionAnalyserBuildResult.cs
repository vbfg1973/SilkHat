using Microsoft.CodeAnalysis;

namespace SilkHat.Domain.CodeAnalysis.DotnetProjects
{
    public record SolutionAnalyserBuildResult(WorkspaceDiagnosticKind DiagnosticKind, string Message);
}