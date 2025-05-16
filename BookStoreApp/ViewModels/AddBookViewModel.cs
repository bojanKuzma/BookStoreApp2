using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using BookStoreApp.Data;
using BookStoreApp.Models;
using BookStoreApp.Resources;
using BookStoreApp.Services;
using BookStoreApp.UI;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BookStoreApp.ViewModels;

public partial class AddBookViewModel : ObservableValidator
{
    private readonly AppDbContext _dbContext;
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(Strings))]
    private string _title;
    
    [ObservableProperty]
    private string _shortDescription;
    
    [ObservableProperty]
    [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(Strings))]
    private string _content;

    [ObservableProperty] private List<Genre> _availableGenres = new();

    [ObservableProperty] private List<Author> _availableAuthors = new();

    [ObservableProperty] private ObservableCollection<ItemViewModel<Genre>> _selectedGenres;
    
    [ObservableProperty] private ObservableCollection<ItemViewModel<Author>> _selectedAuthors;
    
    [ObservableProperty]
    private string _selectedGenresText = string.Empty;
    
    [ObservableProperty]
    private string _selectedAuthorsText = string.Empty;
    
    [ObservableProperty]
    private string _imageUrl;

    private Book book;

    public AddBookViewModel(AppDbContext dbContext, INavigationService navigationService, Book? book)
    {
        _dbContext = dbContext;
        _navigationService = navigationService;
        AvailableAuthors = _dbContext.Authors.ToList();
        AvailableGenres = _dbContext.Genres.ToList();
        if (book != null)
        {
            Title = book.Title;
            ShortDescription = book.ShortDescription;
            Content = book.Content;
            ImageUrl = book.ImageUrl;
            this.book = book;
        }
        SelectedGenres = new ObservableCollection<ItemViewModel<Genre>>();
        foreach (var genre in AvailableGenres)
        {
            var item = new ItemViewModel<Genre>
            { 
                Name = genre.Name, 
                IsSelected = book?.Genres.Any(g => g.Id == genre.Id) ?? false,
                item = genre
            };
            
            item.PropertyChanged += (sender, args) => 
            {
                if (args.PropertyName == nameof(ItemViewModel<Genre>.IsSelected))
                {
                    UpdateSelectedSelectedGenresText();
                }
            };
            
            SelectedGenres.Add(item);
        }
        
        UpdateSelectedSelectedGenresText();
        
        SelectedAuthors = new ObservableCollection<ItemViewModel<Author>>();
        foreach (var author in AvailableAuthors)
        {
            var item = new ItemViewModel<Author>
            { 
                Name = $"{author.FirstName} {author.LastName}", 
                IsSelected = book?.Authors.Any(g => g.Id == author.Id) ?? false,
                item = author
            };
            
            item.PropertyChanged += (sender, args) => 
            {
                if (args.PropertyName == nameof(ItemViewModel<Author>.IsSelected))
                {
                    UpdateSelectedSelectedAuthorsText();
                }
            };
            
            SelectedAuthors.Add(item);
        }

        UpdateSelectedSelectedAuthorsText();

    }
    
    private void UpdateSelectedSelectedGenresText()
    {
        var selectedGenres = SelectedGenres.Where(i => i.IsSelected).Select(i => i.Name);
        SelectedGenresText = string.Join(", ", selectedGenres);
    }
    
    private void UpdateSelectedSelectedAuthorsText()
    {
        var selectedAuthors = SelectedAuthors.Where(i => i.IsSelected).Select(i => i.Name);
        SelectedAuthorsText = string.Join(", ", selectedAuthors);
    }


    [RelayCommand]
    private void SaveBook()
    {
        ValidateAllProperties();

        if (HasErrors) return;
        if (book != null)
        {
            var existingBook = _dbContext.Books.Find(book.Id);
            if (existingBook != null)
            {
                existingBook.Title = Title;
                existingBook.ShortDescription = ShortDescription;
                existingBook.Content = Content;
                existingBook.ImageUrl = ImageUrl;
                existingBook.Genres.Clear();
                existingBook.Authors.Clear();
                _dbContext.SaveChanges();
                existingBook.Genres = SelectedGenres
                    .Where(i => i.IsSelected)
                    .Select(i => i.item)
                    .ToList();
                existingBook.Authors = SelectedAuthors
                    .Where(i => i.IsSelected)
                    .Select(i => i.item)
                    .ToList();
                _dbContext.SaveChanges();
            }
        }
        else
        {
            var newBook = new Book
            {
                Title = Title,
                ShortDescription = ShortDescription,
                Content = Content,
                ImageUrl = ImageUrl,
                Authors = SelectedAuthors
                    .Where(i => i.IsSelected)
                    .Select(i => i.item)
                    .ToList(),
                Genres = SelectedGenres
                    .Where(i => i.IsSelected)
                    .Select(i => i.item)
                    .ToList()
            };
            _dbContext.Books.Add(newBook);
            _dbContext.SaveChanges();
        }
        Cancel();
    }

    [RelayCommand]
    private void Cancel()
    {
        _navigationService.NavigateTo<BookViewModel>();
    }
}