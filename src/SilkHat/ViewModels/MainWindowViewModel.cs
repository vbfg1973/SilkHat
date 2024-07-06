using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using SilkHat.Domain.CodeAnalysis.DotnetProjects;

namespace SilkHat.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private int _count;
        private readonly ISolutionAnalyser _solutionAnalyser;

        [ObservableProperty] private string _textBlockName = "Initial value";

        public ObservableCollection<SolutionViewModel> Solutions { get;  } = new();
        
        public MainWindowViewModel(ISolutionAnalyserFactory solutionAnalyserFactory)
        {
            Solutions.Add(new SolutionViewModel(solutionAnalyserFactory, @"O:\data\explore\student-profiles-api\StudentProfiles.Api.sln"));
            Solutions.Add(new SolutionViewModel(solutionAnalyserFactory, @"O:\data\explore\student-desktop\StudentDesktop.sln"));
            Solutions.Add(new SolutionViewModel(solutionAnalyserFactory, @"O:\data\explore\explore-membership-backend\ExploreMembership-Backend.sln"));
            
            SolutionAnalyserOptions solutionAnalyzerOptions =
                new SolutionAnalyserOptions(@"O:\data\explore\student-profiles-api\StudentProfiles.Api.sln");
            _solutionAnalyser = solutionAnalyserFactory.Create(solutionAnalyzerOptions);
        }

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
            await _solutionAnalyser.LoadSolution();
            await _solutionAnalyser.BuildSolution();
        }
    }
}