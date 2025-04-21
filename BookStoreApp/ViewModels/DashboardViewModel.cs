using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BookStoreApp.ViewModels;

public partial class DashboardViewModel : ObservableObject
{
    [ObservableProperty] private ObservableCollection<string> _recentItems =
    [
        "Project Alpha",
        "Project Beta",
        "Project Gamma"
    ];

    [ObservableProperty] private string _statusMessage = "All systems operational";
}