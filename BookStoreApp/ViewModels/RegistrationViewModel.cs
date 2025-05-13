using System.ComponentModel.DataAnnotations;
using BookStoreApp.Data;
using BookStoreApp.Models;
using BookStoreApp.Resources;
using BookStoreApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.ViewModels;

public partial class RegistrationViewModel : ObservableValidator
{
    private readonly AppDbContext _context;
    
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(RegisterCommand))]
    [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(Strings))]
    [EmailAddress(ErrorMessageResourceName = "EmailValidFormatError", ErrorMessageResourceType = typeof(Strings))]
    private string _email;

    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(RegisterCommand))]
    [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(Strings))]
    private string _username;
    
    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(RegisterCommand))]
    [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(Strings))]
    private string _password;
    
    [ObservableProperty]
    private string _errorMessage;
    
    [ObservableProperty]
    private string _successMessage;

    public RegistrationViewModel(AppDbContext context)
    {
        _context = context;
    }


    [RelayCommand(CanExecute = nameof(CanRegister))]
    private async void Register()
    {
        ValidateAllProperties();
        try
        {
            bool userExists = await _context.Users
                .AnyAsync(u => u.Username == Username || u.Email == Email);

            if (userExists)
            {
                SuccessMessage = string.Empty;
                ErrorMessage = Strings.RegistrationExistingError;
                return;
            }
            
            var (hash, salt) = PasswordService.HashPassword(Password);
            var newUser = new User
            {
                Username = Username,
                Email = Email,
                PasswordHash = hash,
                PasswordSalt = salt,
                Role = "User"
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            ErrorMessage = string.Empty;
            SuccessMessage = Strings.SuccessRegistrationMessage;
        }
        catch (Exception ex)
        {
            SuccessMessage = string.Empty;
            ErrorMessage = $"Registration failed: {ex.Message}";
        }
    }

    public bool CanRegister()
    {
        return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password);
    }

    public void SetPassword(string password)
    {
        Password = password;
        RegisterCommand.NotifyCanExecuteChanged();
    }
}