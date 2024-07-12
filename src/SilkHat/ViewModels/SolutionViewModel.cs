using CommunityToolkit.Mvvm.ComponentModel;
using SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions;
using SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions.SolutionAnalysers.Models;

namespace SilkHat.ViewModels
{
    public partial class SolutionViewModel : ViewModelBase
    {
        private readonly ISolutionCollection _solutionCollection;

        [ObservableProperty] private SolutionModel _solutionModel;

        public SolutionViewModel(SolutionModel solutionModel, ISolutionCollection solutionCollection)
        {
            _solutionCollection = solutionCollection;
            SolutionModel = solutionModel;
        }
    }
}