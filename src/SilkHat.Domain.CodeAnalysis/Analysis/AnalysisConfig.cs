namespace SilkHat.Domain.CodeAnalysis.Analysis
{
    public class AnalysisConfig
    {
        public AnalysisConfig(string solution)
        {
            Solution = solution;
        }

        public string Solution { get; }
    }
}