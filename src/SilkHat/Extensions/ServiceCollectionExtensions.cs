using Microsoft.Extensions.DependencyInjection;
using SilkHat.Verbs.Test;

namespace SilkHat.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCliVerbs(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<TestVerb>();
        }

        public static void AddCustomServices(this IServiceCollection serviceCollection)
        {
        }
    }
}