using System.ComponentModel.DataAnnotations;
using BookStoreApp.Data;
using BookStoreApp.Models;
using BookStoreApp.Resources;
using BookStoreApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;

namespace BookStoreApp.ViewModels;

public partial class AddGenreViewModel :ObservableValidator
{
    private readonly AppDbContext _dbContext;
    private readonly INavigationService _navigationService;
    

    [ObservableProperty]
    [MaxLength(50, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(Strings))]
    [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(Strings))]
    [CustomValidation(typeof(AddGenreViewModel), nameof(ValidateNameUnique))]
    private string _name;
    
    public static ValidationResult ValidateNameUnique(string value, ValidationContext context)
    {
        var dbContext = Ioc.Default.GetService<AppDbContext>();
        if (dbContext.Genres.Any(u => u.Name == value))
        {
            return new ValidationResult(Strings.GenreAlreadyExists);
        }
        return ValidationResult.Success;
    }
    

    private readonly Genre genre;

    public AddGenreViewModel(AppDbContext dbContext, INavigationService navigationService, Genre? genre)
    {
        _dbContext = dbContext;
        _navigationService = navigationService;
        if (genre != null)
        {
           Name = genre.Name;
           this.genre = genre;
        }
    }


    [RelayCommand]
    private void SaveGenre()
    {
        ValidateAllProperties();

        if (HasErrors) return;
        if (genre != null)
        {
            var existingGenre = _dbContext.Genres.Find(genre.Id);
            if (existingGenre != null)
            {
                existingGenre.Name = Name;
                _dbContext.SaveChanges();
            }
        }
        else
        {
            var newGenre = new Genre
            {
                Name = Name,
            };
            _dbContext.Genres.Add(newGenre);
            _dbContext.SaveChanges();
        }
        Cancel();
    }

    [RelayCommand]
    private void Cancel()
    {
        _navigationService.NavigateTo<GenresViewModel>();
    }
}