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

        public string Name { get; set; } = null!;
        public string FullPath { get; set; } = null!;
        public ObservableCollection<SolutionTreeNodeViewModel> Children { get; } = new();
        public NodeType Type { get; set; }
    }
}