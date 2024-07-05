using Microsoft.Extensions.DependencyInjection;
using SilkHat.Cli.Experiments.Verbs.Solution;

namespace SilkHat.Cli.Experiments.Verbs
{
    public static class Configure
    {
        public static IServiceCollection ConfigureVerbs(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<SolutionVerb>();

            return serviceCollection;
        }
    }
}