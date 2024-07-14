using SilkHat.Domain.Graph.GraphEngine.Abstract;

namespace SilkHat.Domain.Graph.GraphEngine
{
    public class TripleGraphAnalyserFactory(ITripleGraph tripleGraph) : ITripleGraphAnalyserFactory
    {
        private readonly ITripleGraph _tripleGraph = tripleGraph;

        public T CreateTripleGraphAnalyzer<T>() where T : BaseTripleGraphAnalyzer
        {
            T graphAnalyzer = (T)Activator.CreateInstance(typeof(T))!;
            _tripleGraph.SetGraph(graphAnalyzer);

            return graphAnalyzer;
        }
    }
}