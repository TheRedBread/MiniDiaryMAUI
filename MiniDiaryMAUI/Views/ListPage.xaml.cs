using MiniDiaryMAUI.Services;
using MiniDiaryMAUI.ViewModel;

namespace MiniDiaryMAUI.Views;

public partial class ListPage : ContentPage
{
    private readonly ListPageViewModel _viewModel;
    public ListPage(IEntriesService entriesService)
	{
		InitializeComponent();

        _viewModel = new ListPageViewModel(entriesService);
        BindingContext = _viewModel;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        Console.WriteLine("MAIN PAGE APPEARING");

        try
        {
            await _viewModel.LoadAllEntriesAsync();
            Console.WriteLine("LOAD FINISHED");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"APPEARING ERROR: {ex}");
        }
    }
}