using MiniDiaryMAUI.Services;
using MiniDiaryMAUI.ViewModels;

namespace MiniDiaryMAUI.Views;

public partial class MainPage : ContentPage
{
    public MainPage(IEntriesService entriesService)
    {
        InitializeComponent();

        Console.WriteLine("cops");

        BindingContext = new MainViewModel(entriesService);
    }
}
