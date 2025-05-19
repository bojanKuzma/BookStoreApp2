using System.ComponentModel.DataAnnotations;
using BookStoreApp.Data;
using BookStoreApp.Models;
using BookStoreApp.Resources;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BookStoreApp.ViewModels;

public partial class AddBookRequestViewModel : ObservableValidator
{
    private readonly AppDbContext _dbContext;

    [ObservableProperty] private string _author;

    [ObservableProperty]
    [Range(1000, 9999, ErrorMessageResourceName = "RangeError", ErrorMessageResourceType = typeof(Strings))]
    private int _publicationYear = DateTime.Now.Year;

    [ObservableProperty]
    [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(Strings))]
    private string _title;
    
    [ObservableProperty]
    private string _notificationMessage;

    public AddBookRequestViewModel(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    [RelayCommand]
    private async Task Save()
    {
        ClearErrors();
        ValidateAllProperties();

        if (HasErrors)
            return;

        var bookRequest = new BookRequest
        {
            Title = Title,
            Author = Author,
            PublicationYear = PublicationYear
        };
        _dbContext.BookRequests.Add(bookRequest);
        await _dbContext.SaveChangesAsync();
        NotificationMessage = Strings.NotificationMessage;
    }
}