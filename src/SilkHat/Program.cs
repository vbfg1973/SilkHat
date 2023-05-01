using CommandLine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SilkHat.Extensions;
using SilkHat.Verbs.Test;

public static class Program
{
    private static IConfiguration _sConfiguration = null!;
    private static IServiceCollection _sServiceCollection = null!;
    private static IServiceProvider _sServiceProvider = null!;

    public static void Main(string[] args)
    {
        BuildConfiguration();
        Log.Logger = new LoggerConfiguration()
            .ReadFrom
            .Configuration(_sConfiguration)
            .CreateLogger();
        ConfigureServices();

        Parser.Default
            .ParseArguments<
                // AnalyseOptions, 
                // GitOptions, 
                TestOptions
            >(args)
            .WithParsed(options =>
                {
                    var verb = _sServiceProvider.GetService<TestVerb>();
                    verb?.Run(options).Wait();
                }
            )
            ;
    }

    private static void ConfigureServices()
    {
        _sServiceCollection = new ServiceCollection();

        _sServiceCollection.AddLogging(configure => configure.AddSerilog());

        _sServiceCollection.AddCustomServices();

        _sServiceCollection.AddCliVerbs();

        _sServiceProvider = _sServiceCollection.BuildServiceProvider();
    }

    private static void BuildConfiguration()
    {
        var configuration = new ConfigurationBuilder();

        _sConfiguration = configuration.AddJsonFile("appsettings.json", true, true)
            .AddEnvironmentVariables()
            .Build();
    }
}