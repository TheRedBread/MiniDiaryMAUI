using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MiniDiaryMAUI.Services;

namespace MiniDiaryMAUI.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IEntriesService _entriesService;
    [ObservableProperty]
    private string weightTileText = "Waga";

    [ObservableProperty]
    private string notesTileText = "Notatki";

    public MainViewModel(IEntriesService entriesService)
    {
        _entriesService = entriesService;

        LoadLatestEntries();
    }
    private async void LoadLatestEntries()
    {
        var latestWeight = await _entriesService.GetLatestWeightAsync();

        if (latestWeight != null)
        {
            WeightTileText =
                $"Waga\n{latestWeight.Weight} kg · {latestWeight.dateTime}";
        }

        var latestNote = await _entriesService.GetLatestNoteAsync();

        if (latestNote != null)
        {
            NotesTileText =
                $"Notatki\n{latestNote.Text} · {latestNote.dateTime}";
        }
    }

    [RelayCommand]
    private async Task OpenWeight()
    {
        await Shell.Current.GoToAsync("EntriesPage?type=Weight");
    }

    [RelayCommand]
    private async Task OpenNotes()
    {
        await Shell.Current.GoToAsync("EntriesPage?type=Note");
    }
}