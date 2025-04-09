using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace BookStoreApp.Services;

public partial class NavigationService : ObservableObject, INavigationService
{
    [ObservableProperty]
    private ObservableObject? _currentView;

    public void NavigateTo<T>() where T : ObservableObject
    {
        CurrentView = Ioc.Default.GetService<T>();
    }
}