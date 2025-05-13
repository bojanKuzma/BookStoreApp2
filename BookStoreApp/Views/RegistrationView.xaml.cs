using System.Windows;
using System.Windows.Controls;
using BookStoreApp.ViewModels;

namespace BookStoreApp.Views;

public partial class RegistrationView : UserControl
{
    public RegistrationView()
    {
        InitializeComponent();
    }

    private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
    {
        if (DataContext is RegistrationViewModel viewModel)
        {
            viewModel.SetPassword(PasswordBox.Password);
        }
    }
}