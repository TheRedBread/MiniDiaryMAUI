using MiniDiaryMAUI.Models;

namespace MiniDiaryMAUI.Views;

public partial class ListPage : ContentPage
{
	public ListPage()
	{
		InitializeComponent();
		BindingContext = new ViewModel.ListPageViewModel();
	}
}