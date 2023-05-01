using BenchmarkDotNet.Running;
using SilkHat.Utilities.Benchmarking.Complexity.Indentation;

public class Program
{
    public static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<IndentationBenchmarks>();
    }
}