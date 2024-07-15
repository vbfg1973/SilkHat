﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AvaloniaEdit.Document;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions;
using SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions.SolutionAnalysers.Models;
using SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions.SolutionAnalysers.ProjectStructure;
using SilkHat.Domain.Graph.GraphEngine.GraphAnalysers.Models;

namespace SilkHat.ViewModels
{
    public partial class SolutionViewModel : ViewModelBase
    {
        private readonly ISolutionCollection _solutionCollection;

        [ObservableProperty] private EnhancedDocumentModel _enhancedDocumentModel;

        [ObservableProperty] private bool _isSolutionFileTreePaneOpen = true;
        [ObservableProperty] private bool _isSolutionTabbedPaneOpen;
        [ObservableProperty] private SolutionTreeNodeViewModel _selectedNode;
        [ObservableProperty] private SolutionModel _solutionModel;
        [ObservableProperty] private SyntaxStructureViewModel _syntaxStructure;
        [ObservableProperty] private TextDocument _textDocument = new();

        public SolutionViewModel(SolutionModel solutionModel, ISolutionCollection solutionCollection)
        {
            _solutionCollection = solutionCollection;
            SolutionModel = solutionModel;

            MapSolutionToTreeStructure().Wait();
        }

        public ObservableCollection<TypeDefinitionViewModel> TypeDefinitions { get; set; } = new();
        public ObservableCollection<SolutionTreeNodeViewModel> Nodes { get; } = new();

        [RelayCommand]
        private async Task TriggerSolutionFileTreePane()
        {
            IsSolutionFileTreePaneOpen = !IsSolutionFileTreePaneOpen;
        }

        [RelayCommand]
        public async Task TriggerSolutionTabbedPane()
        {
            IsSolutionTabbedPaneOpen = !IsSolutionTabbedPaneOpen;
        }

        partial void OnSelectedNodeChanged(SolutionTreeNodeViewModel value)
        {
            if (value.Type != SolutionTreeNodeViewModel.NodeType.File) return;

            EnhancedDocumentModel =
                _solutionCollection.GetEnhancedDocument(value.ProjectModel, value.FullPath).Result;

            TextDocument.Text = EnhancedDocumentModel.SourceText;

            PopulateTypeDefinitions(value).Wait();

            SyntaxStructure =
                new SyntaxStructureViewModel(_solutionCollection.SyntaxStructure(value.ProjectModel, value.FullPath)
                    .Result);
            // Console.WriteLine(JsonSerializer.Serialize(SyntaxStructure));
        }

        private async Task PopulateTypeDefinitions(SolutionTreeNodeViewModel value)
        {
            List<TypeDefinition> typeDefinitions =
                await _solutionCollection.GetPathStructure(value.ProjectModel, value.FullPath);

            TypeDefinitions.Clear();

            foreach (TypeDefinition t in typeDefinitions)
            {
                TypeDefinitions.Add(new TypeDefinitionViewModel(t));
            }
        }

        #region Map Solution To Tree Structure

        private async Task MapSolutionToTreeStructure()
        {
            foreach (ProjectModel project in (await _solutionCollection.ProjectsInSolution(SolutionModel)).OrderBy(x =>
                         x.Name))
            {
                ProjectStructureModel projectStructure = await _solutionCollection.ProjectStructure(project);

                if (TryMapStructureToTreeNode(projectStructure,
                        out SolutionTreeNodeViewModel solutionTreeNodeViewModel))
                    Nodes.Add(solutionTreeNodeViewModel);
            }
        }

        private bool TryMapStructureToTreeNode(ProjectStructureModel projectStructureModel,
            out SolutionTreeNodeViewModel solutionTreeNodeViewModel)
        {
            solutionTreeNodeViewModel = new SolutionTreeNodeViewModel(projectStructureModel);

            foreach (ProjectStructureModel child in projectStructureModel.Children.OrderBy(x => x.ProjectStructureType)
                         .ThenBy(x => x.Name))
            {
                if (TryMapStructureToTreeNode(child, out SolutionTreeNodeViewModel childNode))
                    solutionTreeNodeViewModel.Children.Add(childNode);
            }

            return true;
        }

        #endregion
    }
}