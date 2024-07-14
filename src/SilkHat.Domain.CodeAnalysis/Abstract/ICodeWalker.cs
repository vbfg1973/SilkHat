using SilkHat.Domain.Graph.SemanticTriples.Triples.Abstract;

namespace SilkHat.Domain.CodeAnalysis.Abstract
{
    public interface ICodeWalker
    {
        IEnumerable<Triple> Walk();
    }
}