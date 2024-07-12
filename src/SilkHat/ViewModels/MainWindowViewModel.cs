﻿using System;
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
    public partial class MainWindowViewModel : ViewModelBase
    {
        private readonly ISolutionCollection _solutionCollection;

        [ObservableProperty] private int _solutionCount;

        [ObservableProperty] private bool _canLoadSolution = true;
        [ObservableProperty] private bool _isPaneOpen = false;

        [ObservableProperty] private ViewModelBase _currentPage = new HomePageViewModel();
        
        public MainWindowViewModel(ISolutionCollection solutionCollection)
        {
            _solutionCollection = solutionCollection;
        }

        public ObservableCollection<SolutionTree.SolutionTreeViewModel> LoadedSolutions { get; set; } = new();

        [RelayCommand]
        private void ToggleSplitViewPane()
        {
            IsPaneOpen = !IsPaneOpen;
        }
        
        [RelayCommand]
        private async Task MenuFileOpen()
        {
            // Start async operation to open the dialog.
            IReadOnlyList<IStorageFile> files = await App.TopLevel.StorageProvider.OpenFilePickerAsync(
                new FilePickerOpenOptions
                {
                    Title = "Open Text File",
                    AllowMultiple = false
                });

            if (files.Count > 0)
            {
                CanLoadSolution = false;
                
                IStorageFile file = files.First();

                Console.WriteLine($"{file.Name} - {file.Path} - {file.Path.LocalPath}");

                await _solutionCollection.AddSolution(file.Path.LocalPath);

                LoadedSolutions.Clear();

                foreach (SolutionModel solutionModel in await _solutionCollection.SolutionsInCollection())
                {
                    LoadedSolutions.Add(new SolutionTree.SolutionTreeViewModel(solutionModel, _solutionCollection));
                }

                SolutionCount = (await _solutionCollection.SolutionsInCollection()).Count;

                CanLoadSolution = true;
            }
        }

        [RelayCommand]
        private async Task MenuFileExit()
        {
            Environment.Exit(0);
        }
    }
}