using DoToo.ViewModels;

namespace DoToo.Views;

public partial class ItemView : ContentPage
{
    public ItemView(ItemViewModel viewmodel)
    {
        InitializeComponent();
        viewmodel.Navigation = Navigation;
        BindingContext = viewmodel;
    }
}