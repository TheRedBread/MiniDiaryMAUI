using MiniDiaryMAUI.Models;

namespace MiniDiaryMAUI.Services;

public class EntriesService : IEntriesService
{
    private readonly IDatabaseService _database;

    public EntriesService(IDatabaseService database)
    {
        _database = database;
    }

    public async Task<List<EntryWeight>> GetWeightsAsync()
    {
        var entries = await _database.GetAllAsync<EntryWeight>();

        return entries
            .OrderByDescending(x => x.DateTime)
            .ToList();
    }

    public async Task<List<EntryNote>> GetNotesAsync()
    {
        var entries = await _database.GetAllAsync<EntryNote>();

        return entries
            .OrderByDescending(x => x.DateTime)
            .ToList();
    }

    public async Task<EntryWeight?> GetLatestWeightAsync()
    {
        var entries = await GetWeightsAsync();
        return entries.FirstOrDefault();
    }

    public async Task<EntryNote?> GetLatestNoteAsync()
    {
        var entries = await GetNotesAsync();
        return entries.FirstOrDefault();
    }

    public async Task<List<EntryWeight>> GetWeightsSinceAsync(int days)
    {
        var cutoff = DateTime.Now.AddDays(-days);
        var entries = await _database.GetAllAsync<EntryWeight>();

        return entries
            .Where(w => w.DateTime >= cutoff)
            .OrderBy(w => w.DateTime)
            .ToList();
    }
}