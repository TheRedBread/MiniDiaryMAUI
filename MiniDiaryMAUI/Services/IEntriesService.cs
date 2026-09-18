using MiniDiaryMAUI.Models;

namespace MiniDiaryMAUI.Services;

public interface IEntriesService
{
    Task<List<EntryWeight>> GetWeightsAsync();
    Task<List<EntryNote>> GetNotesAsync();

    Task<EntryWeight?> GetLatestWeightAsync();
    Task<EntryNote?> GetLatestNoteAsync();
    Task<List<EntryWeight>> GetWeightsSinceAsync(int days);

    Task<EntryNote?> GetNoteByIdAsync(int id);
    Task<EntryWeight?> GetWeightByIdAsync(int id);

    Task<EntryNote?> EditNoteAsync(EntryNote note);
    Task<EntryWeight?> EditWeightAsync(EntryWeight weight);

    Task<EntryNote?> InsertNoteAsync(EntryNote note);   
    Task<EntryWeight?> InsertWeightAsync(EntryWeight weight);
}
