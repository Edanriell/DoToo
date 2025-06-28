using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DoToo.Models;
using DoToo.Repositories;
using DoToo.Views;

namespace DoToo.ViewModels;

public partial class MainViewModel : ViewModel
{
    private readonly ITodoItemRepository repository;
    private readonly IServiceProvider services;

    [ObservableProperty] private ObservableCollection<TodoItemViewModel> items;
    [ObservableProperty] private TodoItemViewModel selectedItem;

    public MainViewModel(ITodoItemRepository repository, IServiceProvider services)
    {
        repository.OnItemAdded += (sender, item) =>
            items.Add(CreateTodoItemViewModel(item));

        repository.OnItemUpdated += (sender, item) =>
            Task.Run(async () => await LoadDataAsync());

        this.repository = repository;
        this.services = services;
        Task.Run(async () => await LoadDataAsync());
    }

    [RelayCommand]
    public async Task AddItemAsync() { await Navigation.PushAsync(services.GetRequiredService<ItemView>()); }

    private async Task LoadDataAsync()
    {
        var items = await repository.GetItemsAsync();

        var itemViewModels = items.Select(i =>
            CreateTodoItemViewModel(i));

        Items = new ObservableCollection<TodoItemViewModel>
            (itemViewModels);
    }

    private TodoItemViewModel CreateTodoItemViewModel(TodoItem item)
    {
        var itemViewModel = new TodoItemViewModel(item);
        itemViewModel.ItemStatusChanged += ItemStatusChanged;
        return itemViewModel;
    }

    private void ItemStatusChanged(object sender, EventArgs e) { }

    partial void OnSelectedItemChanging(TodoItemViewModel value)
    {
        if (value is not null) return;
        MainThread.BeginInvokeOnMainThread(async () => { await NavigateToItemAsync(value); });
    }

    private async Task NavigateToItemAsync(TodoItemViewModel item)
    {
        var itemView = services.GetRequiredService<ItemView>();
        var vm = itemView.BindingContext as ItemViewModel;

        vm.Item = item.Item;
        itemView.Title = "Edit todo item";

        await Navigation.PushAsync(itemView);
    }
}