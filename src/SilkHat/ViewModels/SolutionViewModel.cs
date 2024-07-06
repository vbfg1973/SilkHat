using System.IO;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SilkHat.Domain.CodeAnalysis.DotnetProjects;
using SilkHat.Domain.Common;

namespace SilkHat.ViewModels
{
    public partial class SolutionViewModel : ViewModelBase
    {
        private readonly ISolutionAnalyserFactory _solutionAnalyserFactory;
        private ISolutionAnalyser _solutionAnalyser;
        private readonly string _solutionPath;
        
        [ObservableProperty]
        private string _solutionNameFromPath;

        public SolutionViewModel(ISolutionAnalyserFactory solutionAnalyserFactory, string solutionPath)
        {
            _solutionAnalyserFactory = solutionAnalyserFactory;
            _solutionPath = solutionPath;

            _solutionNameFromPath = PathUtilities.RemoveExtension(Path.GetFileName(_solutionPath));
        }

        [RelayCommand]
        private async Task LoadSolution()
        {
            _solutionAnalyser = _solutionAnalyserFactory.Create(new SolutionAnalyserOptions(_solutionPath));
            await _solutionAnalyser.LoadSolution();
        }
        
        [RelayCommand]
        private async Task BuildSolution()
        {
            if (_solutionAnalyser != null && _solutionAnalyser.IsLoaded)
            {
                await _solutionAnalyser.BuildSolution();
            }
        }
    }
}