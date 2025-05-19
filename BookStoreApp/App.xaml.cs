using System.Globalization;
using System.Windows;
using BookStoreApp.Data;
using BookStoreApp.Services;
using BookStoreApp.ViewModels;
using BookStoreApp.Views;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BookStoreApp;

public partial class App : Application
{
    public App()
    {
        Ioc.Default.ConfigureServices(
            new ServiceCollection()
                .AddDbContext<AppDbContext>(options =>
                    options.UseSqlite("Data Source=BookStore.db"),
                    contextLifetime: ServiceLifetime.Transient,
                    optionsLifetime: ServiceLifetime.Singleton)
                // Services
                .AddSingleton<INavigationService, NavigationService>()
                .AddSingleton<IThemeService, ThemeService>()
                .AddSingleton<IAuthenticationService, AuthenticationService>()
                // ViewModels
                .AddTransient<LoginViewModel>()
                .AddTransient<RegistrationViewModel>()
                .AddTransient<MainViewModel>()
                .AddTransient<HomeViewModel>()
                .AddTransient<DashboardViewModel>()
                .AddTransient<SettingsViewModel>()
                .AddTransient<AddUserViewModel>()
                .AddTransient<AuthorViewModel>()
                .AddTransient<AddAuthorViewModel>()
                .AddTransient<GenresViewModel>()
                .AddTransient<AddGenreViewModel>()
                .AddTransient<BookViewModel>()
                .AddTransient<AddBookViewModel>()
                .AddTransient<BookListViewModel>()
                .AddTransient<OwnedBooksModelView>()
                .AddTransient<ReadBookViewModel>()
                .AddTransient<BookOrdersViewModel>()
                .AddTransient<BookRequestsViewModel>()
                .AddTransient<AddBookRequestViewModel>()
                // Views
                .AddTransient<HomeView>()
                .AddTransient<DashboardView>()
                .AddTransient<SettingsView>()
                .AddTransient<LoginView>()
                .AddTransient<RegistrationView>()
                .AddTransient<AddUserView>()
                .AddTransient<AuthorView>()
                .AddTransient<AddAuthorView>()
                .AddTransient<GenreView>()
                .AddTransient<AddGenreView>()
                .AddTransient<BookView>()
                .AddTransient<AddBookView>()
                .AddTransient<BookListView>()
                .AddTransient<OwnedBooksView>()
                .AddTransient<ReadBookView>()
                .AddTransient<BookOrdersView>()
                .AddTransient<AddBookRequestView>()
                .AddTransient<BookRequestsView>()
                .BuildServiceProvider());
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        using var dbContext = Ioc.Default.GetRequiredService<AppDbContext>();
        dbContext.Database.Migrate(); 
        
        var mainWindow = new MainWindow()
        {
            DataContext = Ioc.Default.GetRequiredService<MainViewModel>()
        };
        mainWindow.Show();
    }
}
