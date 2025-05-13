using System.ComponentModel.DataAnnotations;
using BookStoreApp.Data;
using BookStoreApp.Models;
using BookStoreApp.Resources;
using BookStoreApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BookStoreApp.ViewModels;

public partial class AddAuthorViewModel : ObservableValidator
{
    private readonly AppDbContext _dbContext;
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(Strings))]
    private DateTime _dateOfBirth;

    [ObservableProperty]
    [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(Strings))]
    private string _firstName;

    [ObservableProperty]
    [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(Strings))]
    private string _lastName;

    private readonly Author author;

    public AddAuthorViewModel(AppDbContext dbContext, INavigationService navigationService, Author? author)
    {
        _dbContext = dbContext;
        _navigationService = navigationService;
        if (author != null)
        {
            FirstName = author.FirstName;
            LastName = author.LastName;
            DateOfBirth = author.DateOfBirth;
            this.author = author;
        }
    }


    [RelayCommand]
    private void SaveAuthor()
    {
        ValidateAllProperties();

        if (HasErrors) return;
        if (author != null)
        {
            var existingAuthor = _dbContext.Authors.Find(author.Id);
            if (existingAuthor != null)
            {
                existingAuthor.FirstName = FirstName;
                existingAuthor.LastName = LastName;
                existingAuthor.DateOfBirth = DateOfBirth;
                _dbContext.SaveChanges();
                Cancel();
            }
        }
        else
        {
            var newAuthor = new Author
            {
                FirstName = FirstName,
                LastName = LastName,
                DateOfBirth = DateOfBirth
            };
            _dbContext.Authors.Add(newAuthor);
            _dbContext.SaveChanges();
        }
        Cancel();
    }

    [RelayCommand]
    private void Cancel()
    {
        _navigationService.NavigateTo<AuthorViewModel>();
    }
}