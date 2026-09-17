using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MiniDiaryMAUI.ViewModel;

public partial class MainPageViewModel : ObservableObject
{
    [RelayCommand]
    async Task GoToTest()
    {
        await Shell.Current.GoToAsync("//TestPage");
    }
}
