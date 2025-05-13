using BookStoreApp.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookStoreApp.Services;

public interface IAuthenticationService
{
    Task<User?> Login(string username, string password);
    void Logout();
    User? CurrentUser { get; }
    event Action<User?>? CurrentUserChanged;
    bool IsLoggedIn { get; }

    public Task UpdateUserAsync(User user);
}