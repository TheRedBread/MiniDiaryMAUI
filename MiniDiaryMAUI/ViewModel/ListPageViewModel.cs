using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MiniDiaryMAUI.ViewModel;

[QueryProperty(nameof(Mode), "mode")]
public partial class ListPageViewModel : ObservableObject
{
    [ObservableProperty]
    private string mode;

    [ObservableProperty]
    private bool isNotes;

    [ObservableProperty]
    private bool isWeights;

    partial void OnModeChanged(string value)
    {
        if (value == "notes")
        {
            IsNotes = true;
            IsWeights = false;
        }
        else if (value == "weights")
        {
            IsNotes = false;
            IsWeights = true;
        }
    }
    [RelayCommand]
    private async Task GoToMainPage()
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
}