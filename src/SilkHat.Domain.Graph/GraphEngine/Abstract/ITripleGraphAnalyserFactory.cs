namespace SilkHat.Domain.Graph.GraphEngine.Abstract
{
    public interface ITripleGraphAnalyserFactory
    {
        T CreateTripleGraphAnalyzer<T>() where T : BaseTripleGraphAnalyzer;
    }
}