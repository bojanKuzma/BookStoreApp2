using System.Collections.ObjectModel;
using BookStoreApp.Data;
using BookStoreApp.Models;
using BookStoreApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;

namespace BookStoreApp.ViewModels;

public partial class GenresViewModel : ObservableObject
{
    private readonly AppDbContext _dbContext;
    private readonly INavigationService _navigationService;
    
    [ObservableProperty]
    private ObservableCollection<Genre> _genres;
    
    [ObservableProperty]
    private Genre _selectedGenre;
    
    [ObservableProperty]
    private string _searchText;

    
    public GenresViewModel(AppDbContext dbContext, INavigationService navigationService)
    {
        _dbContext = dbContext;
        _navigationService = navigationService;
        LoadGenres();
    }

    private void LoadGenres()
    {
        Genres = new ObservableCollection<Genre>(_dbContext.Genres.ToList());
    }
    
    [RelayCommand]
    private void AddGenre()
    {
        var viewModel = new AddGenreViewModel(_dbContext, _navigationService, null);
        _navigationService.NavigateToViewModel(viewModel);
    }

    [RelayCommand]
    private void UpdateGenre()
    {
        if (SelectedGenre == null) return;
        var viewModel = new AddGenreViewModel(_dbContext, _navigationService, SelectedGenre);
        _navigationService.NavigateToViewModel(viewModel);
    }

    [RelayCommand]
    private void DeleteGenre(Genre genre)
    {
        if (genre == null) return;
        _dbContext.Genres.Remove(genre);
        _dbContext.SaveChanges();
        Genres.Remove(genre);
        DialogHost.Close("RootDialog");
    }
    
    partial void OnSearchTextChanged(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            LoadGenres();
        }
        else
        {
            Genres = new ObservableCollection<Genre>(_dbContext.Genres
                .Where(u => u.Name.Contains(value))
                .ToList());
        }
    }
}