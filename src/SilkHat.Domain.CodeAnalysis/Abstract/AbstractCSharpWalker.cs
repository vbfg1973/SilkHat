using Microsoft.CodeAnalysis.CSharp;
using SilkHat.Domain.Graph.SemanticTriples.Triples.Abstract;

namespace SilkHat.Domain.CodeAnalysis.Abstract
{
    public abstract class AbstractCSharpWalker : CSharpSyntaxWalker, ICodeWalker
    {
        public abstract IEnumerable<Triple> Walk();
    }
}