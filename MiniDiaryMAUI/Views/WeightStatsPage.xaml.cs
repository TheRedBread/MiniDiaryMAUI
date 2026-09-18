using MiniDiaryMAUI.Services;
using MiniDiaryMAUI.ViewModels;

namespace MiniDiaryMAUI.Views;

public partial class WeightStatsPage : ContentPage
{
    private readonly WeightStatsViewModel _viewModel;

    public WeightStatsPage(IEntriesService entriesService)
    {
        InitializeComponent();

        _viewModel = new WeightStatsViewModel(entriesService);
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadAsync();
    }

    private async void OnBackTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}