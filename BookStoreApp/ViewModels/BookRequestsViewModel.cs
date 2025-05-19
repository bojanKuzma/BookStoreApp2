using System.Collections.ObjectModel;
using BookStoreApp.Data;
using BookStoreApp.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookStoreApp.ViewModels;

public partial class BookRequestsViewModel : ObservableObject
{
    private readonly AppDbContext _dbContext;

    [ObservableProperty]
    private ObservableCollection<BookRequest> _bookRequests;

    public BookRequestsViewModel(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        var requests = _dbContext.BookRequests
            .OrderByDescending(r => r.RequestDate)
            .ToList();
        BookRequests = new ObservableCollection<BookRequest>(requests);
    }
}