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
    }

    public async Task LoadLatestEntriesAsync()
    {
        Console.WriteLine("LOAD LATEST ENTRIES STARTED");

        try
        {
            Console.WriteLine("GETTING WEIGHT...");

            var latestWeight = await _entriesService.GetLatestWeightAsync();

            Console.WriteLine("GET WEIGHT FINISHED");

            if (latestWeight == null)
            {
                Console.WriteLine("NO WEIGHT FOUND");
            }
            else
            {
                Console.WriteLine(
                    $"WEIGHT FOUND: {latestWeight.Weight}, {latestWeight.DateTime}");

                WeightTileText =
                    $"Waga\n{latestWeight.Weight} kg · {latestWeight.DateTime}";
            }

            Console.WriteLine("GETTING NOTE...");

            var latestNote = await _entriesService.GetLatestNoteAsync();

            Console.WriteLine("GET NOTE FINISHED");

            if (latestNote != null)
            {
                NotesTileText =
                    $"Notatki\n{latestNote.Text} · {latestNote.DateTime}";
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"LOAD ERROR: {ex}");
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