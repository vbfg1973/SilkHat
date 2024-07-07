﻿using Microsoft.Extensions.DependencyInjection;
using SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions;
using SilkHat.Domain.CodeAnalysis.DotnetProjects.Solutions.SolutionAnalysers;

namespace SilkHat.Domain.CodeAnalysis
{
    public static class ConfigureServices
    {
        public static IServiceCollection ConfigureCodeAnalysisServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ISolutionAnalyserFactory, SolutionAnalyserFactory>();
            serviceCollection.AddSingleton<ISolutionCollection, SolutionCollection>();

            return serviceCollection;
        }
    }
}