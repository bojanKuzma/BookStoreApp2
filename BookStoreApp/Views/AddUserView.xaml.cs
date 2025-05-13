using System.Windows;
using System.Windows.Controls;
using BookStoreApp.ViewModels;

namespace BookStoreApp.Views;

public partial class AddUserView : UserControl
{
    public AddUserView()
    {
        InitializeComponent();
    }

    private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        if (DataContext is AddUserViewModel viewModel && sender is PasswordBox passwordBox)
        {
            viewModel.Password = passwordBox.Password;
        }
    }
}