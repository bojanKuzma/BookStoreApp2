using System.Windows.Controls;
using BookStoreApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;

namespace BookStoreApp.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    private readonly IAuthenticationService _authService = Ioc.Default.GetRequiredService<IAuthenticationService>();

    [ObservableProperty] private bool _errorMessage;

    [ObservableProperty] private bool _isLoading;
    
    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
    private string _username = string.Empty;
    
    public LoginViewModel()
    {
        _authService.Logout();
    }
    
    [RelayCommand(CanExecute = nameof(CanLogin))]
    private async Task Login(PasswordBox password)
    {
        IsLoading = true;
        var user = await _authService.Login(Username, password.Password);
        IsLoading = false;
        if (user == null) 
            ErrorMessage = true;
    }

    private bool CanLogin()
    {
        return !string.IsNullOrWhiteSpace(Username);
        // todo add password validation
    }
    
}