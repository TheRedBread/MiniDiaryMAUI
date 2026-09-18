using MiniDiaryMAUI.Services;
using MiniDiaryMAUI.ViewModel;
using MiniDiaryMAUI.ViewModels;

namespace MiniDiaryMAUI.Views;

public partial class EditPage : ContentPage
{
	private readonly EditViewModel _viewModel;
    public EditPage(IEntriesService entriesService)
	{
		InitializeComponent();

        _viewModel = new EditViewModel(entriesService);
        BindingContext = _viewModel;
    }
}