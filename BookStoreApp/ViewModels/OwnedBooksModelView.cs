using System.Collections.ObjectModel;
using BookStoreApp.Data;
using BookStoreApp.Models;
using BookStoreApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.ViewModels;

public partial class OwnedBooksModelView : ObservableObject
{
    private readonly AppDbContext _dbContext;
    private readonly IAuthenticationService _authenticationService;
    private readonly INavigationService _navigationService;

    public ObservableCollection<Book> Books { get; private set; }

    public OwnedBooksModelView(AppDbContext dbContext, IAuthenticationService authenticationService,
        INavigationService navigationService)
    {
        _dbContext = dbContext;
        _authenticationService = authenticationService;
        _navigationService = navigationService;
        LoadUnorderedBooks();
    }
    
    
    private void LoadUnorderedBooks()
    {
        var bookIds = _dbContext.BookOrders
            .Where(order => order.UserId == _authenticationService.CurrentUser.Id && order.IsApproved == true)
            .Select(order => order.BookId)
            .ToList();
        Books = new ObservableCollection<Book>(_dbContext.Books
            .Where(book => bookIds.Contains(book.Id))
            .Include(b => b.Authors)
            .Include(b => b.Genres)
            .ToList());
    }
    
    [RelayCommand]
    private void ReadBook(Book book)
    {
        if (book == null) return;
        _navigationService.NavigateToViewModel(new ReadBookViewModel(_navigationService, book));
    }
}