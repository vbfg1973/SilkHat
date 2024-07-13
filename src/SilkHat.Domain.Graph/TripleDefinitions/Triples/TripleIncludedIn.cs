using SilkHat.Domain.Graph.TripleDefinitions.Nodes;
using SilkHat.Domain.Graph.TripleDefinitions.Relationships;
using SilkHat.Domain.Graph.TripleDefinitions.Triples.Abstract;

namespace SilkHat.Domain.Graph.TripleDefinitions.Triples
{
    public class TripleIncludedIn : Triple
    {
        public TripleIncludedIn(
            ProjectNode contentA,
            FolderNode contentNodeB)
            : base(contentA, contentNodeB, new IncludedInRelationship())
        {
        }

        public TripleIncludedIn(
            FolderNode contentA,
            FolderNode contentNodeB)
            : base(contentA, contentNodeB, new IncludedInRelationship())
        {
        }

        public TripleIncludedIn(
            FileNode contentA,
            FolderNode contentNodeB)
            : base(contentA, contentNodeB, new IncludedInRelationship())
        {
        }
    }
}