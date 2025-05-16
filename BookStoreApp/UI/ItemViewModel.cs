using CommunityToolkit.Mvvm.ComponentModel;

namespace BookStoreApp.UI;

public partial class ItemViewModel<T> : ObservableObject
{
    [ObservableProperty] 
    private string _name;

    [ObservableProperty]
    private bool _isSelected;
    
    public T item;
    
    public override string ToString()
    {
        return Name ?? string.Empty;
    }
}