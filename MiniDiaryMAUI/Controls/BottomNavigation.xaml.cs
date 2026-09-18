namespace MiniDiaryMAUI.Controls;

public partial class BottomNavigation : ContentView
{
    public BottomNavigation()
    {
        InitializeComponent();
    }

    private async void OnHomeClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//home");
    }

    private async void OnCalendarClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("calendar");
    }

    private async void OnAddClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("add");
    }

    private async void OnNotificationsClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("notifications");
    }

    private async void OnProfileClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("profile");
    }
}