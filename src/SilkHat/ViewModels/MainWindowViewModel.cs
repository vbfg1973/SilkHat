using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions;
using SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions.SolutionAnalysers.Models;

namespace SilkHat.ViewModels
{
    public partial class MainWindowViewModel(ISolutionCollection solutionCollection) : ViewModelBase
    {
        private readonly Dictionary<SolutionModel, SolutionViewModel> _solutionViewModels = new();
        [ObservableProperty] private bool _canLoadSolution = true;
        [ObservableProperty] private ViewModelBase _currentPage = new HomePageViewModel();
        [ObservableProperty] private bool _isPaneOpen;
        [ObservableProperty] private SolutionModel? _selectedListItem;

        [ObservableProperty] private int _solutionCount;

        public ObservableCollection<SolutionModel> LoadedSolutions { get; set; } = new();

        [RelayCommand]
        private async Task TriggerPane()
        {
            IsPaneOpen = !IsPaneOpen;
        }

        [RelayCommand]
        private async Task MenuAddSolution()
        {
            // Start async operation to open the dialog.
            IReadOnlyList<IStorageFile> files = await App.TopLevel.StorageProvider.OpenFilePickerAsync(
                new FilePickerOpenOptions
                {
                    Title = "Add Solution",
                    AllowMultiple = false
                });

            if (files.Count > 0)
            {
                CanLoadSolution = false;

                IStorageFile file = files.First();

                Console.WriteLine($"{file.Name} - {file.Path} - {file.Path.LocalPath}");

                SolutionModel solutionModel = await solutionCollection.AddSolution(file.Path.LocalPath);
                SolutionViewModel vm = new(solutionModel, solutionCollection);
                _solutionViewModels.TryAdd(solutionModel, vm);

                if (!LoadedSolutions.Any()) CurrentPage = vm;

                LoadedSolutions.Add(solutionModel);

                SolutionCount = (await solutionCollection.SolutionsInCollection()).Count;

                CanLoadSolution = true;
            }
        }

        partial void OnSelectedListItemChanged(SolutionModel? value)
        {
            if (value is null) return;

            if (_solutionViewModels.TryGetValue(value, out SolutionViewModel? vm)) CurrentPage = vm;
        }

        [RelayCommand]
        private async Task MenuFileExit()
        {
            Environment.Exit(0);
        }
    }
}