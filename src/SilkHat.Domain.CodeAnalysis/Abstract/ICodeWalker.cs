using SilkHat.Domain.Graph.TripleDefinitions.Triples.Abstract;

namespace SilkHat.Domain.CodeAnalysis.Abstract
{
    public interface ICodeWalker
    {
        IEnumerable<Triple> Walk();
    }
}