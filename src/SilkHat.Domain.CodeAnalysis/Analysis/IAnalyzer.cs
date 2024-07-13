using SilkHat.Domain.Graph.TripleDefinitions.Triples.Abstract;

namespace SilkHat.Domain.CodeAnalysis.Analysis
{
    public interface IAnalyzer
    {
        Task<IList<Triple>> Analyze();
    }
}