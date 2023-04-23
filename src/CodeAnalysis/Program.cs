using CodeAnalysis;
using CodeAnalysis.Extensions;
using CodeAnalysis.Verbs.Test;
using CommandLine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

public static class Program
{
    private static IConfiguration s_configuration;
    private static IServiceCollection s_serviceCollection;
    private static IServiceProvider s_serviceProvider;
    
    public static void Main(string[] args)
    {
        BuildConfiguration();
        Log.Logger = new LoggerConfiguration()
            .ReadFrom
            .Configuration(s_configuration)
            .CreateLogger();
        ConfigureServices();
        
        Parser.Default
            .ParseArguments<TestOptions>(args)
            .WithParsed<TestOptions>(options =>
            {
                var verb = s_serviceProvider.GetService<TestVerb>();
                verb?.Run(options);
            });
    }
    
    private static void ConfigureServices()
    {
        s_serviceCollection = new ServiceCollection();
        
        s_serviceCollection.AddLogging(configure => configure.AddSerilog());
        s_serviceCollection.AddCliVerbs();

        s_serviceProvider = s_serviceCollection.BuildServiceProvider();
    }

    private static void BuildConfiguration()
    {
        var configuration = new ConfigurationBuilder();
        
        s_configuration = configuration.AddJsonFile("appsettings.json", true, true)
            .AddEnvironmentVariables()
            .Build();
    }
}