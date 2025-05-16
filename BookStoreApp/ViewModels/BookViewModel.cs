using System.Collections.ObjectModel;
using BookStoreApp.Data;
using BookStoreApp.Models;
using BookStoreApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.ViewModels;

public partial class BookViewModel : ObservableObject
{
    private readonly AppDbContext _dbContext;
    private readonly INavigationService _navigationService;
    
    [ObservableProperty]
    private ObservableCollection<Book> _books;
    
    [ObservableProperty]
    private Book _selectedBook;
    
    [ObservableProperty]
    private string _searchText;

    
    public BookViewModel(AppDbContext dbContext, INavigationService navigationService)
    {
        _dbContext = dbContext;
        _navigationService = navigationService;
        LoadBooks();
    }

    private void LoadBooks()
    {
        Books = new ObservableCollection<Book>(_dbContext.Books.ToList());
    }
    
    [RelayCommand]
    private void AddBook()
    {
        var viewModel = new AddBookViewModel(_dbContext, _navigationService, null);
        _navigationService.NavigateToViewModel(viewModel);
    }

    [RelayCommand]
    private void UpdateBook()
    {
        if (SelectedBook == null) return;
        Book b = _dbContext.Books
            .Include(b => b.Genres) 
            .Include(b => b.Authors)
            .First(b => b.Id == SelectedBook.Id);
        var viewModel = new AddBookViewModel(_dbContext, _navigationService, b);
        _navigationService.NavigateToViewModel(viewModel);
    }

    [RelayCommand]
    private void DeleteBook(Book book)
    {
        if (book == null) return;
        _dbContext.Books.Remove(book);
        _dbContext.SaveChanges();
        Books.Remove(book);
        DialogHost.Close("RootDialog");
    }
    
    partial void OnSearchTextChanged(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            LoadBooks();
        }
        else
        {
            Books = new ObservableCollection<Book>(_dbContext.Books
                .Where(u => u.Title.Contains(value))
                .ToList());
        }
    }
}