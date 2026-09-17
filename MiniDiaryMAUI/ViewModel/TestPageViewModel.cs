using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MiniDiaryMAUI.ViewModel
{
    public partial class TestPageViewModel : ObservableObject
    {
        [RelayCommand]
        async Task GoToStart()
        {
            await Shell.Current.GoToAsync("//MainPage");
        }
    }
}
