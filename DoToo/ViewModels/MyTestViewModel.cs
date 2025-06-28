using CommunityToolkit.Mvvm.ComponentModel;

namespace DoToo.ViewModels;

// public class MyTestViewModel : ViewModel
// {
//     private string name;
//
//     public string Name
//     {
//         get => name;
//         set
//         {
//             if (name != value)
//             {
//                 name = value;
//                 RaisePropertyChanged(nameof(Name));
//             }
//         }
//     }
// }

public partial class MyTestViewModel : ViewModel
{
    [ObservableProperty] private string name;
}