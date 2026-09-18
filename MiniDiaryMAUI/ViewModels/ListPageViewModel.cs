using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MiniDiaryMAUI.Models;
using MiniDiaryMAUI.Services;
using System.Collections.ObjectModel;

namespace MiniDiaryMAUI.ViewModel;

[QueryProperty(nameof(ListMode), "listMode")]
public partial class ListPageViewModel : ObservableObject
{
    private readonly IEntriesService _entriesService;
    [ObservableProperty]
    private string listMode;

    [ObservableProperty]
    private bool isNotes;

    [ObservableProperty]
    private bool isWeights;

    [ObservableProperty]
    private ObservableCollection<EntryWeight> listWeights;

    [ObservableProperty]
    private ObservableCollection<EntryNote> listNotes;

    public ListPageViewModel(IEntriesService entriesService)
    {
        _entriesService = entriesService;
    }
    public async Task LoadAllEntriesAsync()
    {
        var allWeights = await _entriesService.GetWeightsAsync();
        if (allWeights != null)
        {

            ListWeights = new ObservableCollection<EntryWeight>();
            foreach (var weight in allWeights)
            {
                ListWeights.Add(weight);
            }
        }
        var allNotes = await _entriesService.GetNotesAsync();
        if (allNotes != null)
        {
            ListNotes = new ObservableCollection<EntryNote>();
            foreach (var note in allNotes)
            {
                ListNotes.Add(note);
            }
        }
    }
    partial void OnListModeChanged(string value)
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
        await Shell.Current.GoToAsync("//home");
    }
    [RelayCommand]
    private async Task AddEntry()
    {
       await Shell.Current.GoToAsync($"//EditPage");
    }
    [RelayCommand]
    private async Task EditEntry(BaseEntry entry)
    {
        await Shell.Current.GoToAsync($"//EditPage?entryId={entry.Id}");
    }
}