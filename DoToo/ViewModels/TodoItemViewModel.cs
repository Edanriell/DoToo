using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DoToo.Models;

namespace DoToo.ViewModels;

public partial class TodoItemViewModel : ViewModel
{
    [ObservableProperty] private TodoItem item;

    public TodoItemViewModel(TodoItem item) { Item = item; }

    public string StatusText => Item.Completed ? "Reactivate" : "Completed";

    public event EventHandler ItemStatusChanged;
    public event EventHandler<TodoItem> ItemDeleteRequested;

    [RelayCommand]
    private void ToggleCompleted()
    {
        Item.Completed = !Item.Completed;
        ItemStatusChanged?.Invoke(this, new EventArgs());
    }
}