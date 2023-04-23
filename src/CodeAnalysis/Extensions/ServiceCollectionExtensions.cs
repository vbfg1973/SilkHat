using CodeAnalysis.Verbs.Test;
using Microsoft.Extensions.DependencyInjection;

namespace CodeAnalysis.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCliVerbs(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<TestVerb>();
        }
    }
}