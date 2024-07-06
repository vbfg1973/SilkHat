using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SilkHat.Domain.CodeAnalysis.DotnetProjects;

namespace SilkHat.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private int _count;
        private ISolutionAnalyzer _solutionAnalyzer;
        
        public MainWindowViewModel(ISolutionAnalyserFactory solutionAnalyserFactory)
        {
            var solutionAnalyzerOptions =
                new SolutionAnalyserOptions(@"O:\data\explore\explore-membership-backend\ExploreMembership-Backend.sln");
            _solutionAnalyzer = solutionAnalyserFactory.Create(solutionAnalyzerOptions);
        }
        
        [ObservableProperty]
        private string _textBlockName = "Initial value";

        [RelayCommand]
        private void ButtonOnClick()
        {
            _count++;
            Console.Error.WriteLine($"Hello - button clicked and count set to {_count}");
            TextBlockName = $"Clicked {_count}";
        }

        [RelayCommand]
        private async Task BuildSolution()
        {
            await _solutionAnalyzer.BuildSolution();
        }
    }
}