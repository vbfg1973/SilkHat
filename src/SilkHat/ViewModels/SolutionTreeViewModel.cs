using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly ISolutionCollection _solutionCollection;

        public SolutionTreeViewModel(SolutionModel solutionModel, ISolutionCollection solutionCollection)
        {
            _solutionModel = solutionModel;
            _solutionCollection = solutionCollection;

            MapSolutionToTreeStructure().Wait();
        }

        public ObservableCollection<SolutionTreeNodeViewModel> Nodes { get; } = new();

        #region Map Solution To Tree Structure

        private async Task MapSolutionToTreeStructure()
        {
            foreach (ProjectModel project in (await _solutionCollection.ProjectsInSolution(SolutionModel)).OrderBy(x => x.Name))
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