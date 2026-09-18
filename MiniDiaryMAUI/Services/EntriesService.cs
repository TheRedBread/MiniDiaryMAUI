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
    public async Task<EntryNote?> GetNoteByIdAsync(int id)
    {
        return await _database.GetByIdAsync<EntryNote>(id);
    }
    public async Task<EntryWeight?> GetWeightByIdAsync(int id)
    {
        return await _database.GetByIdAsync<EntryWeight>(id);
    }
    public async Task<EntryNote?> EditNoteAsync(EntryNote note)
    {
        await _database.UpdateAsync(note);
        return note;
    }
    public async Task<EntryWeight?> EditWeightAsync(EntryWeight weight)
    {
        await _database.UpdateAsync(weight);
        return weight;
    }
    public async Task<EntryNote?> InsertNoteAsync(EntryNote note)
    {
        await _database.InsertAsync(note);
        return note;
    }
    public async Task<EntryWeight?> InsertWeightAsync(EntryWeight weight)
    {
        await _database.InsertAsync(weight);
        return weight;
    }
}
