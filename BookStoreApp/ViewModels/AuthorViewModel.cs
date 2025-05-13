using System.Collections.ObjectModel;
using BookStoreApp.Data;
using BookStoreApp.Models;
using BookStoreApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;

namespace BookStoreApp.ViewModels;

public partial class AuthorViewModel : ObservableObject
{
    private readonly AppDbContext _dbContext;
    private readonly INavigationService _navigationService;
    
    [ObservableProperty]
    private ObservableCollection<Author> _authors;
    
    [ObservableProperty]
    private Author _selectedAuthor;
    
    [ObservableProperty]
    private string _searchText;

    
    public AuthorViewModel(AppDbContext dbContext, INavigationService navigationService)
    {
        _dbContext = dbContext;
        _navigationService = navigationService;
        LoadAuthors();
    }

    private void LoadAuthors()
    {
        Authors = new ObservableCollection<Author>(_dbContext.Authors.ToList());
    }
    
    [RelayCommand]
    private void AddAuthor()
    {
        var viewModel = new AddAuthorViewModel(_dbContext, _navigationService, null);
        _navigationService.NavigateToViewModel(viewModel);
    }

    [RelayCommand]
    private void UpdateAuthor()
    {
        if (SelectedAuthor == null) return;
        var viewModel = new AddAuthorViewModel(_dbContext, _navigationService, SelectedAuthor);
        _navigationService.NavigateToViewModel(viewModel);
    }

    [RelayCommand]
    private void DeleteAuthor(Author author)
    {
        if (author == null) return;
        _dbContext.Authors.Remove(author);
        _dbContext.SaveChanges();
        Authors.Remove(author);
        DialogHost.Close("RootDialog");
    }
    
    partial void OnSearchTextChanged(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            LoadAuthors();
        }
        else
        {
            Authors = new ObservableCollection<Author>(_dbContext.Authors
                .Where(u => u.FirstName.Contains(value) || u.LastName.Contains(value))
                .ToList());
        }
    }
}