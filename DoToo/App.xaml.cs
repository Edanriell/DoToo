using DoToo.Views;

namespace DoToo;

public partial class App : Application
{
    public App(MainView view)
    {
        InitializeComponent();
        MainPage = new NavigationPage(view);
    }
}