using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using SilkHat.Domain.Graph.GraphEngine.GraphAnalysers.Models;
using SilkHat.Domain.Graph.SemanticTriples.Nodes;
using SilkHat.Domain.Graph.SemanticTriples.Nodes.Abstract;

namespace SilkHat.ViewModels
{
    public partial class TypeDefinitionViewModel : ViewModelBase
    {
        [ObservableProperty] private TypeNode _typeNode;

        public TypeDefinitionViewModel(TypeDefinition typeDefinition)
        {
            TypeNode = typeDefinition.Type;
            MethodNodes = new ObservableCollection<MethodNode>(typeDefinition.Methods);
            PropertyNodes = new ObservableCollection<PropertyNode>(typeDefinition.Properties);
        }

        public ObservableCollection<MethodNode> MethodNodes { get; }
        public ObservableCollection<PropertyNode> PropertyNodes { get; }
    }
}