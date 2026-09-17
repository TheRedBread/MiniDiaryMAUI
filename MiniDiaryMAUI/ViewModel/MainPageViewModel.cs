using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MiniDiaryMAUI.ViewModel;

public partial class MainPageViewModel : ObservableObject
{
    [RelayCommand]
    private async Task GoToWeightListPage()
    {
        await Shell.Current.GoToAsync("//ListPage?mode=weights");
    }
    [RelayCommand]
    private async Task GoToNotesListPage()
    {
        await Shell.Current.GoToAsync("//ListPage?mode=notes");
    }
}
