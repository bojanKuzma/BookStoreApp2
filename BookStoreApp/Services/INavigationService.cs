using CommunityToolkit.Mvvm.ComponentModel;

namespace BookStoreApp.Services;

public interface INavigationService
{
    ObservableObject CurrentView { get; }
    void NavigateTo<T>() where T : ObservableObject;
    void NavigateToViewModel<T>(T viewModel) where T : ObservableObject;
}