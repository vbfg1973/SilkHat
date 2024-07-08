using System.Collections.ObjectModel;

namespace SilkHat.ViewModels
{
    public partial class SolutionTreeNodeViewModel : ViewModelBase
    {
        public enum NodeType
        {
            Project,
            Folder,
            File
        }

        public ObservableCollection<SolutionTreeNodeViewModel> Children { get; } = new();
        public string Name { get; set; } = null!;
        public string FullPath { get; set; } = null!;
        public NodeType Type { get; set; }
    }
}