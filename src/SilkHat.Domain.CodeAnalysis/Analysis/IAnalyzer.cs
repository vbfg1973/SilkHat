using SilkHat.Domain.Graph.SemanticTriples.Triples.Abstract;

namespace SilkHat.Domain.CodeAnalysis.Analysis
{
    public interface IAnalyzer
    {
        Task<IList<Triple>> Analyze();
    }
}