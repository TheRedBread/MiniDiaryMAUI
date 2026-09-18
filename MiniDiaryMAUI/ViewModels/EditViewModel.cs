using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MiniDiaryMAUI.Models;
using MiniDiaryMAUI.Services;

namespace MiniDiaryMAUI.ViewModels;

[QueryProperty(nameof(EditMode), "editMode")]
[QueryProperty(nameof(EntryId), "entryId")]
internal partial class EditViewModel : ObservableObject
{
    public enum entryType { Notes, Weights };

    [ObservableProperty]
    private string editMode;

    [ObservableProperty]
    private string entryId;

    [ObservableProperty]
    private bool isEditMode;

    [ObservableProperty]
    private entryType currentEntryType;

    [ObservableProperty]
    private string inputText;

    [ObservableProperty]
    private DateTime inputDate;

    [ObservableProperty]
    private TimeSpan inputTime;

    [ObservableProperty]
    private DateTime inputDateTime;

    partial void OnEditModeChanged(string value)
    {
        currentEntryType = value == "notes" ? entryType.Notes : entryType.Weights;
    }

    partial void OnEntryIdChanged(string value)
    {
       bool doesEntryExist = false;
        switch (currentEntryType)
        {
            case entryType.Notes:
                if (_entriesService.GetNoteByIdAsync(int.TryParse(value, out int noteId2) ? noteId2 : 0) != null)
                {
                    doesEntryExist = true;
                }
                break;
            case entryType.Weights:
                if (_entriesService.GetWeightByIdAsync(int.TryParse(value, out int weightId2) ? weightId2 : 0) != null)
                {
                    doesEntryExist = true;
                }
                break;
        }
        if (!doesEntryExist || string.IsNullOrEmpty(value))
        {
            isEditMode = false;
        }
        else
        {
            IsEditMode = true;

            _ = LoadEntryAsync();
        }

    }
    private async Task LoadEntryAsync()
    {
        if (IsEditMode)
        {
            switch (currentEntryType)
            {
                case entryType.Notes:
                    var note = await _entriesService.GetNoteByIdAsync(int.TryParse(EntryId, out int noteId) ? noteId : 0);
                    InputText = note?.Text;
                    InputDateTime = note.DateTime;
                    InputTime = InputDateTime.TimeOfDay;
                    InputDate = InputDateTime.Date;
                    break;
                case entryType.Weights:
                    var weight = await _entriesService.GetWeightByIdAsync(int.TryParse(EntryId, out int weightId) ? weightId : 0);
                    InputText = weight?.Weight.ToString();
                    InputDateTime = weight.DateTime;
                    InputTime = InputDateTime.TimeOfDay;
                    InputDate = InputDateTime.Date;
                    break;
            }
        }
        else
        {
            InputDate = DateTime.Now;
            InputTime = DateTime.Now.TimeOfDay;
            InputDateTime = DateTime.Now;
            InputText = string.Empty;
        }
    }

    private IEntriesService _entriesService;
    public EditViewModel(IEntriesService entriesService)
    {
        _entriesService = entriesService;
    }

    [RelayCommand]
    private async Task SaveEntry()
    {
        switch(currentEntryType)
        {
            case entryType.Notes:
                if (IsEditMode)
                {
                    var note = await _entriesService.GetNoteByIdAsync(int.TryParse(EntryId, out int noteId) ? noteId : 0);
                    if (note != null)
                    {
                        InputDateTime = InputDate.Date + InputTime;
                        note.DateTime = InputDateTime;
                        note.Text = InputText;
                        await _entriesService.EditNoteAsync(note);
                    }
                }
                else
                {
                    InputDateTime = InputDate.Date + InputTime;
                    var newNote = new EntryNote
                    {
                        DateTime = InputDateTime,
                        Text = InputText
                    };
                    await _entriesService.InsertNoteAsync(newNote);
                }
                break;
            case entryType.Weights:
                if (IsEditMode)
                {
                    var weight = await _entriesService.GetWeightByIdAsync(int.TryParse(EntryId, out int weightId) ? weightId : 0);
                    if (weight != null)
                    {
                        InputDateTime = InputDate.Date + InputTime;
                        weight.DateTime = InputDateTime;
                        weight.Weight = double.Parse(InputText);
                        await _entriesService.EditWeightAsync(weight);
                    }
                }
                else
                {
                    InputDateTime = InputDate.Date + InputTime;
                    var newWeight = new EntryWeight {
                        Weight = double.Parse(InputText),
                        DateTime = InputDateTime
                    };
                    await _entriesService.InsertWeightAsync(newWeight);
                }
                break;
        }
        InputDate = DateTime.Now;
        InputTime = DateTime.Now.TimeOfDay;
        InputDateTime = DateTime.Now;
        InputText = string.Empty;
        await Shell.Current.GoToAsync("//home");
    }

    [RelayCommand]
    private async Task GoToMainPage()
    {
        await Shell.Current.GoToAsync("//home");
    }
}