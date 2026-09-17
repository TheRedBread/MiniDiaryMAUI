using MiniDiaryMAUI.Services;
using MiniDiaryMAUI.ViewModels;

namespace MiniDiaryMAUI.Views;

public partial class MainPage : ContentPage
{
    private readonly MainViewModel _viewModel;

    public MainPage(IEntriesService entriesService)
    {
        InitializeComponent();

        _viewModel = new MainViewModel(entriesService);
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        Console.WriteLine("MAIN PAGE APPEARING");

        try
        {
            await _viewModel.LoadLatestEntriesAsync();
            Console.WriteLine("LOAD FINISHED");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"APPEARING ERROR: {ex}");
        }
    }
}
