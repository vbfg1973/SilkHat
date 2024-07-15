using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions.SolutionAnalysers.AST;

namespace SilkHat.ViewModels
{
    public partial class SyntaxStructureViewModel : ViewModelBase
    {
        public SyntaxStructureViewModel(SyntaxStructure syntaxStructure)
        {
            Name = syntaxStructure.Name;

            foreach (var child in syntaxStructure.Children)
            {
                Children.Add(new SyntaxStructureViewModel(child));
            }
        }

        [ObservableProperty]
        private string _name;
        
        public ObservableCollection<SyntaxStructureViewModel> Children { get; } = new();
    }
}