using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MiniDiaryMAUI.Services;
using MiniDiaryMAUI.Views;
using System.Diagnostics;

namespace MiniDiaryMAUI.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IEntriesService _entriesService;
    private readonly IDateTimeFormatterService _dateTimeFormatterService;

    [ObservableProperty]
    public partial string WeightValueText { get; set; } = "Brak danych";

    [ObservableProperty]
    public partial string WeightDateText { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string NotesValueText { get; set; } = "Brak danych";

    [ObservableProperty]
    public partial string NotesDateText { get; set; } = string.Empty;

    public MainViewModel(IEntriesService entriesService, IDateTimeFormatterService dateTimeFormatterService)
    {
        _entriesService = entriesService;
        _dateTimeFormatterService = dateTimeFormatterService;
    }

    public async Task LoadLatestEntriesAsync()
    {
        try
        {
            var latestWeight = await _entriesService.GetLatestWeightAsync();
            if (latestWeight != null)
            {
                WeightValueText = $"{latestWeight.Weight} kg";
                WeightDateText = _dateTimeFormatterService.FormatRelative(latestWeight.DateTime);
            }

            var latestNote = await _entriesService.GetLatestNoteAsync();
            if (latestNote != null)
            {
                NotesValueText = latestNote.Text;
                NotesDateText = _dateTimeFormatterService.FormatRelative(latestNote.DateTime);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Failed to load latest entries: {ex}");
        }
    }

    [RelayCommand]
    private async Task OpenWeight()
    {
        await Shell.Current.GoToAsync("//ListPage?listMode=weights");
    }

    [RelayCommand]
    private async Task OpenNotes()
    {
        await Shell.Current.GoToAsync("//ListPage?listMode=notes");
    }
}