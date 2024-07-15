using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using SilkHat.Domain.Graph.GraphEngine.GraphAnalysers.Models;
using SilkHat.Domain.Graph.SemanticTriples.Nodes;
using SilkHat.Domain.Graph.SemanticTriples.Nodes.Abstract;

namespace SilkHat.ViewModels
{
    public partial class TypeDefinitionViewModel : ViewModelBase
    {
        [ObservableProperty] private TypeNode _typeNode;

        [ObservableProperty] private NodeWithLocation<MethodNode> _selectedMethodNode;
        [ObservableProperty] private NodeWithLocation<PropertyNode> _selectedPropertyNode;
        
        public TypeDefinitionViewModel(TypeDefinition typeDefinition)
        {
            TypeNode = typeDefinition.Type;
            MethodNodes = new ObservableCollection<NodeWithLocation<MethodNode>>(typeDefinition.Methods.OrderBy(x => x.Location.Location.Start.Character));
            PropertyNodes = new ObservableCollection<NodeWithLocation<PropertyNode>>(typeDefinition.Properties.OrderBy(x => x.Location.Location.Start.Character));
        }

        public ObservableCollection<NodeWithLocation<MethodNode>> MethodNodes { get; }
        public ObservableCollection<NodeWithLocation<PropertyNode>> PropertyNodes { get; }

        partial void OnSelectedMethodNodeChanged(NodeWithLocation<MethodNode> value)
        {
            Console.WriteLine($"Method: {value.Location.Location}");
        }
        
        partial void OnSelectedPropertyNodeChanged(NodeWithLocation<PropertyNode> value)
        {
            Console.WriteLine($"Property: {value.Location.Location}");
        }
    }
}