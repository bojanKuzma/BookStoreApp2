using System.Collections.ObjectModel;
using BookStoreApp.Models;
using BookStoreApp.Navigation;
using BookStoreApp.Resources;
using BookStoreApp.Services;
using BookStoreApp.Util;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;

namespace BookStoreApp.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private IAuthenticationService _authenticationService = Ioc.Default.GetRequiredService<IAuthenticationService>();

    [ObservableProperty] private List<string> _availableLanguages = LanguageManager.AvailableLanguages;

    [ObservableProperty] private ObservableCollection<NavigationItem> _navigationItems = new();

    [ObservableProperty]
    private INavigationService _navigationService = Ioc.Default.GetRequiredService<INavigationService>();


    [ObservableProperty] private NavigationItem? _selectedItem;

    [ObservableProperty] private string _selectedLanguage = LanguageManager.AvailableLanguages.First();

    public MainViewModel()
    {
        NavigationService.NavigateTo<LoginViewModel>();
        GenerateNavigation(null);
        AuthenticationService.CurrentUserChanged += OnCurrentUserChanged;
    }

    private void OnCurrentUserChanged(User? user)
    {
        GenerateNavigation(user);
    }

    partial void OnSelectedLanguageChanged(string value)
    {
        LanguageManager.ChangeLanguage(value);
        GenerateNavigation(AuthenticationService.CurrentUser);
    }

    private void GenerateNavigation(User? user)
    {
        NavigationItems.Clear();

        switch (user)
        {
            case { Role: "Admin" }:
                NavigationItems.Add(new NavigationItem
                    { Title = Strings.HomeNavigationTitle, Icon = "AccountGroup", ViewModelType = typeof(HomeViewModel) });
                NavigationItems.Add(
                    new NavigationItem
                    {
                        Title = Strings.AuthorsNavigationTitle,
                        Icon = "DrawPen",
                        ViewModelType = typeof(AuthorViewModel)
                    }
                );

                NavigationItems.Add(new NavigationItem
                {
                    Title = Strings.DashboardNavigationTitle, Icon = "Dashboard",
                    ViewModelType = typeof(DashboardViewModel)
                });
                NavigationItems.Add(new NavigationItem
                {
                    Title = Strings.SettingsNavigationTitle, Icon = "Cog", ViewModelType = typeof(SettingsViewModel)
                });
                NavigationItems.Add(new NavigationItem
                    { Title = Strings.LogoutNavigationTitle, Icon = "Logout", ViewModelType = typeof(LoginViewModel) });

                break;
            case { Role: "User" }:
                NavigationItems.Add(new NavigationItem
                    { Title = Strings.HomeNavigationTitle, Icon = "Home", ViewModelType = typeof(HomeViewModel) });
                NavigationItems.Add(new NavigationItem
                {
                    Title = Strings.SettingsNavigationTitle, Icon = "Cog", ViewModelType = typeof(SettingsViewModel)
                });
                NavigationItems.Add(new NavigationItem
                    { Title = Strings.LogoutNavigationTitle, Icon = "Logout", ViewModelType = typeof(LoginViewModel) });

                break;
            default:
                NavigationItems.Add(new NavigationItem
                    { Title = Strings.LoginNavigationTitle, Icon = "Login", ViewModelType = typeof(LoginViewModel) });
                NavigationItems.Add(new NavigationItem
                {
                    Title = Strings.RegistrationNavigationTitle, Icon = "Person",
                    ViewModelType = typeof(RegistrationViewModel)
                });
                break;
        }

        if (user != null && SelectedItem.ViewModelType == typeof(LoginViewModel))
        {
            Navigate(NavigationItems.First());
        }
        else
        {
            NavigationItem? itemToSelect = null;
            var currentSelectedType = SelectedItem?.ViewModelType;
            if (currentSelectedType != null)
                itemToSelect = NavigationItems.FirstOrDefault(item => item.ViewModelType == currentSelectedType);
            itemToSelect ??= NavigationItems.FirstOrDefault();
            SelectedItem = itemToSelect;
        }
    }


    [RelayCommand(CanExecute = nameof(CanNavigate))]
    private void Navigate(NavigationItem item)
    {
        SelectedItem = item;
        var method = typeof(INavigationService)
            .GetMethod(nameof(INavigationService.NavigateTo))?
            .MakeGenericMethod(item.ViewModelType);
        method?.Invoke(NavigationService, null);
    }

    private bool CanNavigate(NavigationItem item)
    {
        return item != SelectedItem;
    }

    partial void OnSelectedItemChanged(NavigationItem? value)
    {
        NavigateCommand.NotifyCanExecuteChanged();
    }
}