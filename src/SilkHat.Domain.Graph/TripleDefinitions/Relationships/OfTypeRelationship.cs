﻿using SilkHat.Domain.Graph.TripleDefinitions.Relationships.Abstract;

namespace SilkHat.Domain.Graph.TripleDefinitions.Relationships
{
    public class OfTypeRelationship : Relationship
    {
        public override string Type => "OF_TYPE";
    }
}