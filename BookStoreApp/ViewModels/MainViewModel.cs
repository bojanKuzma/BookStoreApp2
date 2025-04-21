using System.Collections.ObjectModel;
using System.Windows.Controls;
using BookStoreApp.Navigation;
using BookStoreApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;

namespace BookStoreApp.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private NavigationItem? _selectedItem;
    
    [ObservableProperty]
    private INavigationService _navigationService = Ioc.Default.GetRequiredService<INavigationService>();
    
    public ObservableCollection<NavigationItem> NavigationItems { get; } =
    [
        new() { Title = "Home", Icon = "Home", ViewModelType = typeof(HomeViewModel) },
        new() { Title = "Dashboard", Icon = "ViewDashboard", ViewModelType = typeof(DashboardViewModel) },
        new() { Title = "Settings", Icon = "Cog", ViewModelType = typeof(SettingsViewModel) }
    ];

    public MainViewModel()
    {
        NavigationService.NavigateTo<HomeViewModel>();
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
        => item != SelectedItem;

    partial void OnSelectedItemChanged(NavigationItem? value)
    {
        NavigateCommand.NotifyCanExecuteChanged();
    }
    
}