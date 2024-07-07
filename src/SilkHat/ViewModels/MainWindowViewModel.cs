using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SilkHat.Domain.CodeAnalysis.DotnetProjects.Models;
using SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions;

namespace SilkHat.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase,
        IObserver<SolutionCollection.SolutionLoadedNotification>
    {
        private readonly ISolutionCollection _solutionCollection;
        private int _count;

        [ObservableProperty] private int _solutionCount;

        public MainWindowViewModel(ISolutionCollection solutionCollection)
        {
            _solutionCollection = solutionCollection;
        }

        public ObservableCollection<string> LoadedSolutions { get; set; } = new();

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(SolutionCollection.SolutionLoadedNotification value)
        {
            throw new NotImplementedException();
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
                IStorageFile file = files.First();

                Console.WriteLine($"{file.Name} - {file.Path} - {file.Path.LocalPath}");

                await _solutionCollection.AddSolution(file.Path.LocalPath);

                LoadedSolutions.Clear();

                foreach (SolutionModel solutionModel in await _solutionCollection.SolutionModels())
                {
                    LoadedSolutions.Add(solutionModel.Name);
                }

                SolutionCount = (await _solutionCollection.SolutionModels()).Count;
            }
        }

        [RelayCommand]
        private async Task MenuFileExit()
        {
            Environment.Exit(0);
        }
    }
}