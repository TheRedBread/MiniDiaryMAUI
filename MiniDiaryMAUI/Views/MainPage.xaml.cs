using MiniDiaryMAUI.Services;
using MiniDiaryMAUI.ViewModels;

namespace MiniDiaryMAUI.Views;

public partial class MainPage : ContentPage
{
    private readonly MainViewModel _viewModel;
    private readonly IDateTimeFormatterService _dateTimeFormatterService;

    public MainPage(IEntriesService entriesService, IDateTimeFormatterService dateTimeFormatterService)
    {
        InitializeComponent();

        _viewModel = new MainViewModel(entriesService, dateTimeFormatterService);
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
