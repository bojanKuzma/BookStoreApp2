using System.Collections.ObjectModel;
using BookStoreApp.Data;
using BookStoreApp.Models;
using BookStoreApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.ViewModels;

public partial class BookListViewModel : ObservableObject
{
    private readonly IAuthenticationService _authenticationService;
    private readonly AppDbContext _dbContext;

    public BookListViewModel(AppDbContext dbContext, IAuthenticationService authenticationService)
    {
        _dbContext = dbContext;
        _authenticationService = authenticationService;
        LoadUnorderedBooks();
    }

    public ObservableCollection<Book> Books { get; private set; }

    private void LoadUnorderedBooks()
    {
        var orderedBookIds = _dbContext.BookOrders
            .Where(order => order.UserId == _authenticationService.CurrentUser.Id &&
                            (order.IsApproved == true || order.IsApproved == null))
            .Select(order => order.BookId)
            .ToList();
        var books = _dbContext.Books
            .Include(b => b.Authors)
            .Include(b => b.Genres)
            .Where(book => !orderedBookIds.Contains(book.Id))
            .ToList();
        Books = new ObservableCollection<Book>(books);
    }

    [RelayCommand]
    private void OrderBook(Book book)
    {
        if (book == null) return;

        var order = new BookOrder
        {
            BookId = book.Id,
            UserId = _authenticationService.CurrentUser.Id,
            IsApproved = null
        };

        _dbContext.BookOrders.Add(order);
        _dbContext.SaveChanges();
        Books.Remove(book);
    }
}