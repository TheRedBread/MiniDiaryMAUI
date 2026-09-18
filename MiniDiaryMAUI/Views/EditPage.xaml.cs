using MiniDiaryMAUI.Services;
using MiniDiaryMAUI.ViewModel;
using MiniDiaryMAUI.ViewModels;

namespace MiniDiaryMAUI.Views;

public partial class EditPage : ContentPage
{
	private readonly EditViewModel _viewModel;
    public EditPage(IDatabaseService databaseService)
	{
		InitializeComponent();

        _viewModel = new EditViewModel(databaseService);
        BindingContext = _viewModel;
    }
}