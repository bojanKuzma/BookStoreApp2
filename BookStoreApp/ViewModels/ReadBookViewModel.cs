using System.Windows.Input;
using BookStoreApp.Models;
using BookStoreApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BookStoreApp.ViewModels;

public partial class ReadBookViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    [ObservableProperty]
    private Book _book;
    
    [ObservableProperty]
    private bool _isReadingContent;
    
    public ReadBookViewModel(INavigationService navigationService, Book book)
    {
        _navigationService = navigationService;
        Book = book;
    }
    
    [RelayCommand]
    private void Read(){
        IsReadingContent = true;
    }

    [RelayCommand]
    private void BackToInfo()
    {
        IsReadingContent = false;
    }
    
    [RelayCommand]
    private void Back()
    {
        _navigationService.NavigateTo<OwnedBooksModelView>();
    }
    
}