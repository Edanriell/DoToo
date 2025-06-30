using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DoToo.Models;
using DoToo.Repositories;

namespace DoToo.ViewModels;

public partial class ItemViewModel : ViewModel
{
    private readonly ITodoItemRepository repository;

    [ObservableProperty] private TodoItem item = new();

    public ItemViewModel(ITodoItemRepository repository) { this.repository = repository; }

    // 🔥 Add property to check if item exists in database
    public bool IsExistingItem => Item.Id > 0;

    // 🔥 Add property for button text
    public string SaveButtonText => IsExistingItem ? "Update" : "Add";

    [RelayCommand]
    private async Task Save()
    {
        // 🔥 Ensure required fields are filled
        if (string.IsNullOrWhiteSpace(Item.Title))
        {
            await Application.Current.MainPage.DisplayAlert(
                "Error",
                "Please enter a title for the task",
                "OK");
            return;
        }

        await repository.AddOrUpdateAsync(Item);
        await Navigation.PopAsync();
    }

    [RelayCommand(CanExecute = nameof(CanDelete))] // 🔥 Add CanExecute
    private async Task Delete()
    {
        var shouldDelete = await Application.Current.MainPage.DisplayAlert(
            "Delete Task",
            $"Are you sure you want to delete '{Item.Title}'?",
            "Delete",
            "Cancel");

        if (shouldDelete)
        {
            Debug.WriteLine("🔥 Calling repository.DeleteItemAsync...");
            await repository.DeleteItemAsync(Item);
            Debug.WriteLine("🔥 Repository delete completed!");
            await Navigation.PopAsync();
        }
    }

    // 🔥 Add method to determine if delete can execute
    private bool CanDelete() { return IsExistingItem; }

    // 🔥 Add method to refresh computed properties
    partial void OnItemChanged(TodoItem value)
    {
        OnPropertyChanged(nameof(IsExistingItem));
        OnPropertyChanged(nameof(SaveButtonText));
        DeleteCommand.NotifyCanExecuteChanged(); // Update delete button state
    }
}