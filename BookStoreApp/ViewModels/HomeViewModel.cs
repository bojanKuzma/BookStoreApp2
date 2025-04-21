using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BookStoreApp.ViewModels;

public partial class HomeViewModel : ObservableObject
{
    [ObservableProperty] private int _clickCount;

    [ObservableProperty] private string _welcomeMessage = "Welcome to the Home Page!";

    [RelayCommand]
    private void IncrementCount()
    {
        ClickCount++;
        WelcomeMessage = $"You clicked {ClickCount} times!";
    }
}