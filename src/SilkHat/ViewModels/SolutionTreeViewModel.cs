using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions;
using SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions.SolutionAnalysers;
using SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions.SolutionAnalysers.Models;
using SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions.SolutionAnalysers.ProjectStructure;

namespace SilkHat.ViewModels
{
    public partial class SolutionTreeViewModel : ViewModelBase
    {
        [ObservableProperty] private SolutionModel _solutionModel;

        public SolutionTreeViewModel(SolutionModel solutionModel, ISolutionCollection solutionCollection)
        {
            _solutionModel = solutionModel;

            if (solutionCollection.TryGetSolutionAnalyser(solutionModel.Name, out SolutionAnalyser solutionAnalyser))
                Nodes = MapSolutionToTreeStructure(solutionAnalyser);
        }

        public ObservableCollection<SolutionTreeNodeViewModel> Nodes { get; } = new();

        #region Map Solution To Tree Structure

        private ObservableCollection<SolutionTreeNodeViewModel> MapSolutionToTreeStructure(
            SolutionAnalyser solutionAnalyser)
        {
            ObservableCollection<SolutionTreeNodeViewModel> nodes = new();

            foreach (ProjectModel project in solutionAnalyser.Projects.OrderBy(x => x.Name))
            {
                ProjectStructureModel projectStructure = solutionAnalyser.ProjectStructure(project);

                if (TryMapStructureToTreeNode(projectStructure,
                        out SolutionTreeNodeViewModel solutionTreeNodeViewModel))
                    nodes.Add(solutionTreeNodeViewModel);
            }

            return nodes;
        }

        private bool TryMapStructureToTreeNode(ProjectStructureModel projectStructureModel,
            out SolutionTreeNodeViewModel solutionTreeNodeViewModel)
        {
            solutionTreeNodeViewModel = new SolutionTreeNodeViewModel(projectStructureModel);

            foreach (ProjectStructureModel child in projectStructureModel.Children)
            {
                if (TryMapStructureToTreeNode(child, out SolutionTreeNodeViewModel childNode))
                    solutionTreeNodeViewModel.Children.Add(childNode);
            }

            return true;
        }

        #endregion
    }
}