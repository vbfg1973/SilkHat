using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions;
using SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions.SolutionAnalysers;
using SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions.SolutionAnalysers.Models;

namespace SilkHat.ViewModels
{
    public partial class SolutionTreeViewModel : ViewModelBase
    {
        private readonly ISolutionCollection _solutionCollection;
        private Dictionary<ProjectModel, List<DocumentModel>> _documentsByProject;

        [ObservableProperty] private SolutionModel _solutionModel;

        public ObservableCollection<SolutionTreeNodeViewModel> Nodes { get; }

        public SolutionTreeViewModel(SolutionModel solutionModel, ISolutionCollection solutionCollection)
        {
            _solutionModel = solutionModel;
            _solutionCollection = solutionCollection;

            if (_solutionCollection.TryGetSolutionAnalyser(solutionModel.Name, out SolutionAnalyser solutionAnalyser))
                Nodes = MapSolutionToTreeStructure(solutionAnalyser);
        }

        #region Map Solution To Tree Structure

        private ObservableCollection<SolutionTreeNodeViewModel> MapSolutionToTreeStructure(SolutionAnalyser solutionAnalyser)
        {
            ObservableCollection<SolutionTreeNodeViewModel> nodes = new();
            

            foreach (ProjectModel project in solutionAnalyser.Projects.OrderBy(x => x.Name))
            {
                var projectStructure = solutionAnalyser.ProjectStructure(project);

                Console.WriteLine(JsonSerializer.Serialize(projectStructure));
                
                SolutionTreeNodeViewModel model = new SolutionTreeNodeViewModel
                {
                    Name = project.Name,
                    FullPath = project.Path,
                    Type = SolutionTreeNodeViewModel.NodeType.Project
                };

                nodes.Add(model);
            }

            return nodes;
        }

        #endregion
    }
}