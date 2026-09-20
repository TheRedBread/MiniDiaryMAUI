using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MiniDiaryMAUI.Services;
using MiniDiaryMAUI.Views;

namespace MiniDiaryMAUI.ViewModels;

[QueryProperty(nameof(EntryId), "entryId")]
[QueryProperty(nameof(EntryMode), "entryMode")]
public partial class DetailsViewModel : ObservableObject
{
    private readonly IEntriesService _entriesService;

    [ObservableProperty]
    private string entryId;

    [ObservableProperty]
    private string entryMode;

    [ObservableProperty]
    private bool isNoteEntry;

    [ObservableProperty]
    private string valueText;

    [ObservableProperty]
    private string dateText;

    public DetailsViewModel(IEntriesService entriesService)
    {
        _entriesService = entriesService;
    }

    partial void OnEntryModeChanged(string value)
    {
        IsNoteEntry = value == "notes";
        _ = LoadEntryAsync();
    }

    partial void OnEntryIdChanged(string value)
    {
        _ = LoadEntryAsync();
    }

    private async Task LoadEntryAsync()
    {
        // wait until both query properties have actually arrived
        if (string.IsNullOrEmpty(EntryMode) || !int.TryParse(EntryId, out var id))
            return;

        if (EntryMode == "notes")
        {
            var note = await _entriesService.GetNoteByIdAsync(id);
            if (note == null) return;

            ValueText = note.Text;
            DateText = note.DateTime.ToString("dd.MM.yyyy, HH:mm");
        }
        else
        {
            var weight = await _entriesService.GetWeightByIdAsync(id);
            if (weight == null) return;

            ValueText = $"{weight.Weight} kg";
            DateText = weight.DateTime.ToString("dd.MM.yyyy, HH:mm");
        }
    }

    [RelayCommand]
    private async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task EditThisEntry()
    {
        await Shell.Current.GoToAsync($"{nameof(EditPage)}?entryId={EntryId}&editMode={EntryMode}");
    }
}