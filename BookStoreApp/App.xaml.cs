using System.Windows;
using BookStoreApp.Services;
using BookStoreApp.ViewModels;
using BookStoreApp.Views;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace BookStoreApp;

public partial class App : Application
{
    public App()
    {
        Ioc.Default.ConfigureServices(
            new ServiceCollection()
                // Services
                .AddSingleton<INavigationService, NavigationService>()
                .AddSingleton<IThemeService, ThemeService>()
                // ViewModels
                .AddTransient<MainViewModel>()
                .AddTransient<HomeViewModel>()
                .AddTransient<DashboardViewModel>()
                .AddTransient<SettingsViewModel>()
                // Views
                .AddTransient<HomeView>()
                .AddTransient<DashboardView>()
                .AddTransient<SettingsView>()
                .BuildServiceProvider());
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        var mainWindow = new MainWindow()
        {
            DataContext = Ioc.Default.GetRequiredService<MainViewModel>()
        };
        mainWindow.Show();
    }
}
