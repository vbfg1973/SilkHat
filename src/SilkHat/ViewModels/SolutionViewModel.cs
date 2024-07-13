using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions;
using SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions.SolutionAnalysers.Models;
using SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions.SolutionAnalysers.ProjectStructure;

namespace SilkHat.ViewModels
{
    public partial class SolutionViewModel : ViewModelBase
    {
        private readonly ISolutionCollection _solutionCollection;

        [ObservableProperty] private bool _isSolutionFileTreePaneOpen = true;
        [ObservableProperty] private bool _isSolutionTabbedPaneOpen = false;
        
        [ObservableProperty] private SolutionModel _solutionModel;

        [ObservableProperty] private EnhancedDocumentModel _enhancedDocumentModel;

        public SolutionViewModel(SolutionModel solutionModel, ISolutionCollection solutionCollection)
        {
            _solutionCollection = solutionCollection;
            SolutionModel = solutionModel;

            MapSolutionToTreeStructure().Wait();
        }

        public ObservableCollection<SolutionTreeNodeViewModel> Nodes { get; } = new();

        [ObservableProperty] private SolutionTreeNodeViewModel _selectedNode;

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

            foreach (ProjectStructureModel child in projectStructureModel.Children.OrderBy(x => x.ProjectStructureType).ThenBy(x => x.Name))
            {
                if (TryMapStructureToTreeNode(child, out SolutionTreeNodeViewModel childNode))
                    solutionTreeNodeViewModel.Children.Add(childNode);
            }
            
            return true;
        }

        partial void OnSelectedNodeChanged(SolutionTreeNodeViewModel value)
        {
            if (value.Type == SolutionTreeNodeViewModel.NodeType.File)
            {
                EnhancedDocumentModel = _solutionCollection.GetEnhancedDocument(value.ProjectModel, value.FullPath).Result;
            }
        }

        #endregion
    }
}