using System;
using System.Collections.ObjectModel;
using SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions.SolutionAnalysers.Models;
using SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions.SolutionAnalysers.ProjectStructure;

namespace SilkHat.ViewModels
{
    public class SolutionTreeNodeViewModel(ProjectStructureModel projectStructureModel) : ViewModelBase
    {
        public enum NodeType
        {
            SolutionFolder = 0,
            Project = 10,
            Folder = 20,
            File = 30
        }

        public string Name { get; } = projectStructureModel.Name;
        public string RelativePath { get; } = projectStructureModel.RelativePath;
        public string FullPath { get; } = projectStructureModel.FullPath;
        public ObservableCollection<SolutionTreeNodeViewModel> Children { get; } = new();
        public NodeType Type { get; } = MapType(projectStructureModel.ProjectStructureType);
        public ProjectModel ProjectModel { get; } = projectStructureModel.ProjectModel;

        private static NodeType MapType(ProjectStructureType projectStructureType)
        {
            return projectStructureType switch
            {
                ProjectStructureType.Project => NodeType.Project,
                ProjectStructureType.File => NodeType.File,
                ProjectStructureType.Folder => NodeType.Folder,
                _ => throw new ArgumentOutOfRangeException(nameof(projectStructureType), projectStructureType, null)
            };
        }
    }
}