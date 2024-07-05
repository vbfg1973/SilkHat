using CommandLine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SilkHat.Cli.Experiments.Verbs;
using SilkHat.Cli.Experiments.Verbs.Solution;
using SilkHat.Domain.CodeAnalysis;

namespace SilkHat.Cli.Experiments
{
    public static class Program
    {
        private static IConfiguration s_configuration;
        private static IServiceCollection s_serviceCollection;
        private static IServiceProvider s_serviceProvider;
        
        internal static void Main(string[] args)
        {
            BuildConfiguration();
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(s_configuration)
                .WriteTo.Console()
                .CreateLogger();

            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                Log.Logger.Error(e.ExceptionObject as Exception, "UnhandledException");
            };

            ConfigureServices();

            ParseArgumentsIntoCommandLineVerbs(args);

            Log.CloseAndFlush();
        }
        
        private static void ParseArgumentsIntoCommandLineVerbs(string[] args)
        {
            Parser.Default
                .ParseArguments<
                    SolutionOptions
                >(args)
                .WithParsed<SolutionOptions>(options =>
                {
                    SolutionVerb? verb = s_serviceProvider.GetService<SolutionVerb>();

                    verb?.Run(options)
                        .Wait();
                })
                ;
        }
        
        private static void ConfigureServices()
        {
            s_serviceCollection = new ServiceCollection();

            // AppSettings appSettings = new AppSettings();
            // s_configuration.Bind("Settings", appSettings);

            s_serviceCollection.AddLogging(configure => configure.AddSerilog());

            // Log.Debug("{AppSettings}", JsonSerializer.Serialize(appSettings));

            // s_serviceCollection.AddSingleton(appSettings.Database);

            s_serviceCollection
                .ConfigureCodeAnalysisServices()
                .ConfigureVerbs();

            s_serviceProvider = s_serviceCollection.BuildServiceProvider();
        }

        
        private static void BuildConfiguration()
        {
            ConfigurationBuilder configuration = new();

            s_configuration = configuration.AddJsonFile("appsettings.json", true, true)
                // .AddJsonFile($"appsettings.{environmentName}.json", true, true)
                .AddEnvironmentVariables()
                .Build();
        }
    }
}