using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.Json;
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

            foreach (SolutionTreeNodeViewModel node in Nodes)
            {
                Console.Error.WriteLine(node.Name);
                Console.Error.WriteLine();
                Console.Error.WriteLine(JsonSerializer.Serialize(node));
                Console.Error.WriteLine();
            }
                
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

                // Console.Error.WriteLine(project.AssemblyName);
                // Console.Error.WriteLine(JsonSerializer.Serialize(projectStructure));
                // Console.Error.WriteLine();

                if (TryMapStructureToTreeNode(projectStructure,
                        out SolutionTreeNodeViewModel solutionTreeNodeViewModel))
                {
                    Console.WriteLine(JsonSerializer.Serialize(solutionTreeNodeViewModel));
                    nodes.Add(solutionTreeNodeViewModel);
                }
            }

            return nodes;
        }

        private bool TryMapStructureToTreeNode(ProjectStructureModel projectStructureModel,
            out SolutionTreeNodeViewModel solutionTreeNodeViewModel)
        {
            solutionTreeNodeViewModel = MapNode(projectStructureModel);

            // Console.WriteLine(JsonSerializer.Serialize(solutionTreeNodeViewModel));

            foreach (ProjectStructureModel child in projectStructureModel.Children)
            {
                if (TryMapStructureToTreeNode(child, out SolutionTreeNodeViewModel childNode))
                {
                    solutionTreeNodeViewModel.Children.Add(childNode);
                }
            }

            return true;
        }

        private static SolutionTreeNodeViewModel MapNode(ProjectStructureModel projectStructureModel)
        {
            return new SolutionTreeNodeViewModel
            {
                Name = projectStructureModel.Name,
                RelativePath = projectStructureModel.RelativePath,
                FullPath = projectStructureModel.FullPath,
                Type = MapType(projectStructureModel.ProjectStructureType)
            };
        }


        private static SolutionTreeNodeViewModel.NodeType MapType(ProjectStructureType projectStructureType)
        {
            return projectStructureType switch
            {
                ProjectStructureType.Project => SolutionTreeNodeViewModel.NodeType.Project,
                ProjectStructureType.File => SolutionTreeNodeViewModel.NodeType.File,
                ProjectStructureType.Folder => SolutionTreeNodeViewModel.NodeType.Folder,
                _ => throw new ArgumentOutOfRangeException(nameof(projectStructureType), projectStructureType, null)
            };
        }

        #endregion
    }
}