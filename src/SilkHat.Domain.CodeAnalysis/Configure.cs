using Microsoft.Extensions.DependencyInjection;
using SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions;
using SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions.SolutionAnalysers;
using SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions.SolutionAnalysers.ProjectStructure;
using SilkHat.Domain.Graph.GraphEngine;

namespace SilkHat.Domain.CodeAnalysis
{
    public static class ConfigureServices
    {
        public static IServiceCollection ConfigureCodeAnalysisServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ISolutionAnalyserFactory, SolutionAnalyserFactory>();
            serviceCollection.AddTransient<IProjectStructureBuilder, ProjectStructureBuilder>();
            serviceCollection.AddScoped<ITripleGraph, TripleGraph>();
            serviceCollection.AddSingleton<ISolutionCollection, SolutionCollection>();

            return serviceCollection;
        }
    }
}