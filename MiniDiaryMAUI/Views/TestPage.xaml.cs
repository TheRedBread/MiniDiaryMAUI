namespace MiniDiaryMAUI.Views;

public partial class TestPage : ContentPage
{
	public TestPage()
	{
		InitializeComponent();
        BindingContext = new ViewModel.TestPageViewModel();
    }
}