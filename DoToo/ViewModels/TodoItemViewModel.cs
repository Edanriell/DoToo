using CommunityToolkit.Mvvm.ComponentModel;
using DoToo.Models;

namespace DoToo.ViewModels;

public partial class TodoItemViewModel : ViewModel
{
    [ObservableProperty] private TodoItem item;

    public TodoItemViewModel(TodoItem item) { Item = item; }

    public string StatusText => Item.Completed ? "Reactivate" : "Completed";

    public event EventHandler ItemStatusChanged;
}