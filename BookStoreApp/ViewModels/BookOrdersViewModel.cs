using System.Collections.ObjectModel;
using System.Windows.Media;
using BookStoreApp.Data;
using BookStoreApp.Models;
using BookStoreApp.Resources;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.ViewModels;

public partial class BookOrdersViewModel : ObservableObject
{
    private readonly AppDbContext _dbContext;

    [ObservableProperty] private string? _approvalFilter;

    [ObservableProperty] private string _filterText;

    
    [ObservableProperty] 
    private ObservableCollection<BookOrder> _orders;

    [ObservableProperty] private BookOrder _selectedOrder;
    
    [ObservableProperty] private bool _isDialogOpen;
    [ObservableProperty] private string _dialogTitle;
    [ObservableProperty] private BookOrder _dialogOrder;
    [ObservableProperty] private string _rejectionReason;
    [ObservableProperty] private bool _isRejectionDialog;
    [ObservableProperty] private Brush _dialogButtonBackground;

    public BookOrdersViewModel(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        ApprovalFilter = "All";
        LoadOrders();
    }
    

    private async void LoadOrders()
    {
        var query = _dbContext.BookOrders
            .Include(o => o.Book)
            .Include(o => o.User)
            .AsQueryable();
        if (!string.IsNullOrEmpty(ApprovalFilter))
        {
            query = ApprovalFilter switch
            {
                "Approved" => query.Where(o => o.IsApproved == true),
                "Rejected" => query.Where(o => o.IsApproved == false),
                "Pending" => query.Where(o => o.IsApproved == null),
                _ => query 
            };
        }
        
        if (!string.IsNullOrEmpty(FilterText))
            query = query.Where(o => o.Book.Title.Contains(FilterText) || o.User.Username.Contains(FilterText));
    
        Orders = new ObservableCollection<BookOrder>(await query.ToListAsync());
    }


    [RelayCommand]
    private async void ApproveOrder(BookOrder order)
    {
        if (order != null)
        {
            order.IsApproved = true;
            _dbContext.Update(order);
            await _dbContext.SaveChangesAsync();
            LoadOrders();
        }
    }

    [RelayCommand]
    private async void RejectOrder(BookOrder order)
    {
        if (order != null)
        {
            order.IsApproved = false;
            _dbContext.Update(order);
            await _dbContext.SaveChangesAsync();
            LoadOrders();
        }
    }

    partial void OnApprovalFilterChanged(string? value)
    {
        LoadOrders();
    }
    
    partial void OnFilterTextChanged(string value)
    {
        LoadOrders();
    }
    
    [RelayCommand]
    private void ShowApproveDialog(BookOrder order)
    {
        DialogOrder = order;
        DialogTitle = Strings.ConfirmApproval;
        IsRejectionDialog = false;
        DialogButtonBackground = Brushes.Green;
        IsDialogOpen = true;
    }

    [RelayCommand]
    private void ShowRejectDialog(BookOrder order)
    {
        DialogOrder = order;
        DialogTitle = Strings.ConfirmRejection;
        IsRejectionDialog = true;
        DialogButtonBackground = Brushes.Red;
        IsDialogOpen = true;
    }
    
    
    [RelayCommand]
    private async Task ConfirmDialog()
    {
        if (IsRejectionDialog)
        {
            RejectOrder(DialogOrder);
        }
        else
        {
            ApproveOrder(DialogOrder);
        }
    
        IsDialogOpen = false;
        RejectionReason = string.Empty;
    }

    
}