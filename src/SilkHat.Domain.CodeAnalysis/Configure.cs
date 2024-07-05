using Microsoft.Extensions.DependencyInjection;
using SilkHat.Domain.CodeAnalysis.Solutions;

namespace SilkHat.Domain.CodeAnalysis
{
    public static class ConfigureServices
    {
        public static IServiceCollection ConfigureCodeAnalysisServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ISolutionAnalyserFactory, SolutionAnalyserFactory>();

            return serviceCollection;
        }
    }
}