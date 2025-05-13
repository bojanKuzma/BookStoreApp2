using System.ComponentModel.DataAnnotations;
using BookStoreApp.Data;
using BookStoreApp.Models;
using BookStoreApp.Resources;
using BookStoreApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;

namespace BookStoreApp.ViewModels;

public partial class AddUserViewModel : ObservableValidator
{
    private readonly AppDbContext _dbContext;
    private readonly INavigationService _navigationService;
    
    public static ValidationResult ValidateUsernameUnique(string value, ValidationContext context)
    {
        var dbContext = Ioc.Default.GetService<AppDbContext>();
        if (dbContext.Users.Any(u => u.Username == value))
        {
            return new ValidationResult(Strings.UsernameAlreadyExists);
        }
        return ValidationResult.Success;
    }
    
    public static ValidationResult ValidateEmailUnique(string value, ValidationContext context)
    {
        var dbContext = Ioc.Default.GetService<AppDbContext>();
        if (dbContext.Users.Any(u => u.Email == value))
        {
            return new ValidationResult(Strings.EmailAlreadyExists);
        }
        return ValidationResult.Success;
    }

    [ObservableProperty]
    [Required(ErrorMessageResourceName="FieldRequired", ErrorMessageResourceType = typeof(Strings))]
    [CustomValidation(typeof(AddUserViewModel), nameof(ValidateUsernameUnique))]
    private string _username;

    [ObservableProperty]
    [Required(ErrorMessageResourceName="FieldRequired", ErrorMessageResourceType = typeof(Strings))]
    [EmailAddress(ErrorMessageResourceName = "EmailValidFormatError", ErrorMessageResourceType = typeof(Strings))]
    [CustomValidation(typeof(AddUserViewModel), nameof(ValidateEmailUnique))]
    private string _email;  

    [ObservableProperty]
    [Required(ErrorMessageResourceName="FieldRequired", ErrorMessageResourceType = typeof(Strings))]
    private string _role;
    
    [ObservableProperty]
    [Required(ErrorMessageResourceName="FieldRequired", ErrorMessageResourceType = typeof(Strings))]
    private string _password;
    
    [ObservableProperty]
    private List<string> _roles = new() { "Admin", "User", "Manager" };

    [ObservableProperty] private bool _passwordVisible = true;

    private User user;
    
    public AddUserViewModel(AppDbContext dbContext, INavigationService navigationService, User? user)
    {
        _dbContext = dbContext;
        _navigationService = navigationService;
        if (user != null)
        {
            Username = user.Username;
            Email = user.Email;
            Role = user.Role;
            this.user = user;
            PasswordVisible = false;
        }
        else
        {
            Role = "User";
        }
    }
    
    
    [RelayCommand]
    private void SaveUser()
    {
        ValidateAllProperties();
        
        if (HasErrors)
        {
            return;
        }

        if (user != null)
        {
            var existingUser = _dbContext.Users.Find(user.Id);
            if (existingUser != null)
            {
                existingUser.Username = Username;
                existingUser.Email = Email;
                existingUser.Role = Role;
                _dbContext.SaveChanges();
                Cancel();
            }
        }
        else
        {
            var (hash, salt) = PasswordService.HashPassword(Password);
            var newUser = new User
            {
                Username = Username,
                Email = Email,
                PasswordHash = hash,
                PasswordSalt = salt,
                Role = Role
            };

            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();
            Cancel();
        }
    }
    
    [RelayCommand]
    private void Cancel()
    {
        _navigationService.NavigateTo<HomeViewModel>();
    }
    
    
}