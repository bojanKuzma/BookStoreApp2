using System.Windows.Controls;

namespace BookStoreApp.Views;

public partial class AddBookRequestView : UserControl
{
    public AddBookRequestView()
    {
        InitializeComponent();
        DataContextChanged += (s, e) => 
        {
            if (DataContext is ViewModels.AddBookRequestViewModel vm)
            {
                vm.PropertyChanged += (sender, args) => 
                {
                    if (args.PropertyName == nameof(vm.NotificationMessage) 
                        && !string.IsNullOrEmpty(vm.NotificationMessage))
                    {
                        SnackbarNotification.MessageQueue.Enqueue(vm.NotificationMessage);
                    }
                };
            }
        };
    }
}