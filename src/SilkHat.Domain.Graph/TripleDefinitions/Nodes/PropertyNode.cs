﻿using SilkHat.Domain.Graph.TripleDefinitions.Nodes.Abstract;

namespace SilkHat.Domain.Graph.TripleDefinitions.Nodes
{
    public class PropertyNode : CodeNode
    {
        public PropertyNode(string fullName, string name, string returnType, string[] modifiers) : base(fullName, name,
            modifiers)
        {
            ReturnType = returnType;
        }

        public PropertyNode() : base(string.Empty, string.Empty, new[] { "" })
        {
        }

        public string ReturnType { get; }

        public override string Label { get; } = "Property";
    }
}