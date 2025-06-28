using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DoToo.Models;
using DoToo.Repositories;

namespace DoToo.ViewModels;

public partial class ItemViewModel : ViewModel
{
    private readonly ITodoItemRepository repository;

    [ObservableProperty] private TodoItem item;

    public ItemViewModel(ITodoItemRepository repository)
    {
        this.repository = repository;
        Item = new TodoItem { Due = DateTime.Now.AddDays(1) };
    }

    [RelayCommand]
    public async Task SaveAsync()
    {
        await repository.AddOrUpdateAsync(item);
        await Navigation.PopAsync();
    }
}