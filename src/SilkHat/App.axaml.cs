using System.Data.Common;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SilkHat.Domain.CodeAnalysis;
using SilkHat.ViewModels;
using SilkHat.Views;

namespace SilkHat
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
            
            ServiceCollection collection = new();
            collection.AddTransient<MainWindowViewModel>();
            collection.ConfigureCodeAnalysisServices();
            collection.AddLogging();
            // collection.AddLogging(x => x.AddSerilog());

            // Creates a ServiceProvider containing services from the provided IServiceCollection
            ServiceProvider services = collection.BuildServiceProvider();

            MainWindowViewModel mainWindowViewModel = services.GetRequiredService<MainWindowViewModel>();

            switch (ApplicationLifetime)
            {
                case IClassicDesktopStyleApplicationLifetime desktop:
                    desktop.MainWindow = new MainWindow
                    {
                        DataContext = mainWindowViewModel
                    };
                    break;
                case ISingleViewApplicationLifetime singleViewPlatform:
                    singleViewPlatform.MainView = new MainWindow
                    {
                        DataContext = mainWindowViewModel
                    };
                    break;
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}