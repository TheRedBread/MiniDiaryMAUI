using MiniDiaryMAUI.Models;

namespace MiniDiaryMAUI.Services;

public interface IEntriesService
{
    Task<List<EntryWeight>> GetWeightsAsync();
    Task<List<EntryNote>> GetNotesAsync();

    Task<EntryWeight?> GetLatestWeightAsync();
    Task<EntryNote?> GetLatestNoteAsync();
    Task<List<EntryWeight>> GetWeightsSinceAsync(int days);
}
