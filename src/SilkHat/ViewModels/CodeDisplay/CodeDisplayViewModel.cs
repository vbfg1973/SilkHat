using SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions;
using SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions.SolutionAnalysers.Models;

namespace SilkHat.ViewModels.CodeDisplay
{
    public class CodeDisplayViewModel(ISolutionCollection solutionCollection, DocumentModel documentModel)
        : ViewModelBase
    {
    }
}