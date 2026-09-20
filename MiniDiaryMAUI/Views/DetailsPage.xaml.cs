using MiniDiaryMAUI.Services;
using MiniDiaryMAUI.ViewModels;

namespace MiniDiaryMAUI.Views;

public partial class DetailsPage : ContentPage
{
    private readonly DetailsViewModel _viewModel;

    public DetailsPage(IEntriesService entriesService)
    {
        InitializeComponent();

        _viewModel = new DetailsViewModel(entriesService);
        BindingContext = _viewModel;
    }
}