using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MiniDiaryMAUI.Services;

namespace MiniDiaryMAUI.ViewModels;

[QueryProperty(nameof(EditMode), "editMode")]
internal partial class EditViewModel : ObservableObject
{
    [ObservableProperty]
    private string editMode;

    [ObservableProperty]
    private string editId;

    [ObservableProperty]
    private bool isEditMode;

    private IDatabaseService _databaseService;
    public EditViewModel(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }
    partial void OnEditModeChanged(string value)
    {
        if (value == null)
        {
            EditId = "no id detected";
            IsEditMode = true;
        }
        else
        {
            EditId = value;
            IsEditMode = true;
        }
    }
    [RelayCommand]
    private async Task GoToMainPage()
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
}