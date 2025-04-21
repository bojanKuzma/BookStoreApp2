using System.Windows.Navigation;
using BookStoreApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookStoreApp.Navigation;

public partial class NavigationItem : ObservableObject
{
    [ObservableProperty] private string _icon = string.Empty;
    
    [ObservableProperty] private bool _isSelected;

    [ObservableProperty] private string _title = string.Empty;

    [ObservableProperty] private Type? _viewModelType;
}