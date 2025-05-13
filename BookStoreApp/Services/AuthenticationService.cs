using System.Windows.Media;
using BookStoreApp.Data;
using BookStoreApp.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.Services;

public partial class AuthenticationService(AppDbContext dbContext, IThemeService themeService)
    : ObservableObject, IAuthenticationService
{
    [ObservableProperty] private User? _currentUser;

    public event Action<User?>? CurrentUserChanged;

    public bool IsLoggedIn => CurrentUser != null;

    public async Task<User?> Login(string username, string password)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(user => user.Username == username);
        if (user == null || !PasswordService.VerifyPassword(password, user.PasswordSalt, user.PasswordHash))
            return null;
        CurrentUser = user;
        themeService.IsDarkTheme = user.DarkTheme;
        themeService.PrimaryColor = (Color)ColorConverter.ConvertFromString(user.ThemeColor);
        themeService.ApplyTheme();
        return user;
    }

    public async Task UpdateUserAsync(User user)
    {
        dbContext.Users.Update(user);
        await dbContext.SaveChangesAsync();
        CurrentUser = user;
    }

    public void Logout()
    {
        CurrentUser = null;
    }

    partial void OnCurrentUserChanged(User? value)
    {
        CurrentUserChanged?.Invoke(value);
    }
}