using System.Collections.ObjectModel;
using System.Windows;
using BookStoreApp.Data;
using BookStoreApp.Models;
using BookStoreApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;

namespace BookStoreApp.ViewModels;

public partial class HomeViewModel : ObservableObject
{
   
    private readonly AppDbContext _dbContext;
    private readonly INavigationService _navigationService;
    

    public HomeViewModel(AppDbContext dbContext, INavigationService navigationService)
    {
        _dbContext = dbContext;
        _navigationService = navigationService;
        LoadUsers();
    }
    

    [ObservableProperty]
    private ObservableCollection<User> _users;

    [ObservableProperty]
    private User _selectedUser;
    
    [ObservableProperty]
    private string _searchText;

    
    private void LoadUsers()
    {
        Users = new ObservableCollection<User>(_dbContext.Users.ToList());
    }

    [RelayCommand]
    private void AddUser()
    {
        var viewModel = new AddUserViewModel(_dbContext, _navigationService, null);
        _navigationService.NavigateToViewModel(viewModel);
    }

    [RelayCommand]
    private void UpdateUser()
    {
        if (SelectedUser == null) return;
        var viewModel = new AddUserViewModel(_dbContext, _navigationService, SelectedUser);
        _navigationService.NavigateToViewModel(viewModel);
    }

    [RelayCommand]
    private void DeleteUser(User user)
    {
        if (user == null) return;
        _dbContext.Users.Remove(user);
        _dbContext.SaveChanges();
        Users.Remove(user);
        DialogHost.Close("RootDialog");
    }
    
    partial void OnSearchTextChanged(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            LoadUsers();
        }
        else
        {
            Users = new ObservableCollection<User>(_dbContext.Users
                .Where(u => u.Username.Contains(value) || u.Email.Contains(value) || u.Role.Contains(value))
                .ToList());
        }
    }
   
}