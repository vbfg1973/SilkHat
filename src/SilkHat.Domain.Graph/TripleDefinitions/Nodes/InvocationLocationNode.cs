﻿using SilkHat.Domain.Graph.TripleDefinitions.Nodes.Abstract;

namespace SilkHat.Domain.Graph.TripleDefinitions.Nodes
{
    public class InvocationLocationNode : Node
    {
        public InvocationLocationNode(int location) : base(location.ToString("D10"), location.ToString("D10"))
        {
            Location = location;
        }

        public InvocationLocationNode() : base(string.Empty, string.Empty)
        {
        }

        public int Location { get; }

        public override string Label { get; } = "InvocationLocation";
    }
}