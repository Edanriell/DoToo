using DoToo.Repositories;

namespace DoToo.ViewModels;

public class ItemViewModel : ViewModel
{
    private readonly ITodoItemRepository repository;

    public ItemViewModel(ITodoItemRepository repository) { this.repository = repository; }
}