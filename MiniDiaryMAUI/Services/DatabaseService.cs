using MiniDiaryMAUI.Models;
using SQLite;

namespace MiniDiaryMAUI.Services;

public class DatabaseService : IDatabaseService
{
    private readonly SQLiteAsyncConnection _database;
    private bool _initialized;

    public DatabaseService()
    {
        var databasePath = Path.Combine(
            FileSystem.AppDataDirectory,
            "minidiary.db3");

        _database = new SQLiteAsyncConnection(databasePath);
    }

    public async Task InitializeAsync()
    {
        if (_initialized)
            return;
        
        await _database.DropTableAsync<EntryNote>();
        await _database.DropTableAsync<EntryWeight>();

        await _database.CreateTableAsync<EntryNote>();
        await _database.CreateTableAsync<EntryWeight>();

        await SeedTestDataAsync();

        _initialized = true;
    }

    public async Task<List<T>> GetAllAsync<T>() where T : new()
    {
        await InitializeAsync();

        return await _database
            .Table<T>()
            .ToListAsync();
    }

    public async Task<int> InsertAsync<T>(T entity) where T : new()
    {
        await InitializeAsync();

        return await _database.InsertAsync(entity);
    }

    public async Task<int> UpdateAsync<T>(T entity) where T : new()
    {
        await InitializeAsync();

        return await _database.UpdateAsync(entity);
    }

    public async Task<int> DeleteAsync<T>(T entity) where T : new()
    {
        await InitializeAsync();

        return await _database.DeleteAsync(entity);
    }

    private async Task SeedTestDataAsync()
    {
        var weights = await _database.Table<EntryWeight>().ToListAsync();

        if (weights.Count == 0)
        {
            var random = new Random(42); // fixed seed for repeatable test data
            var startingWeight = 84.0;

            for (var daysAgo = 30; daysAgo >= 0; daysAgo--)
            {
                // small daily fluctuation with a slow downward trend
                var drift = -0.03; // gradual loss per day on average
                var noise = (random.NextDouble() - 0.5) * 0.6; // +/- 0.3 kg noise

                startingWeight += drift + noise;

                await _database.InsertAsync(new EntryWeight
                {
                    Weight = Math.Round(startingWeight, 1),
                    DateTime = DateTime.Now.AddDays(-daysAgo)
                });
            }
        }

        var notes = await _database.Table<EntryNote>().ToListAsync();

        if (notes.Count == 0)
        {
            var sampleNotes = new[]
            {
            "Started tracking my weight.",
            "Feeling good today.",
            "Went for a long walk.",
            "Skipped breakfast, felt sluggish.",
            "Great workout session.",
            "Ate out, probably over my calories.",
            "Slept well, feeling energized.",
            "Stressful day at work.",
            "Tried a new recipe, very healthy.",
            "Rest day, took it easy."
        };

            for (var daysAgo = 28; daysAgo >= 0; daysAgo -= 3)
            {
                var note = sampleNotes[(28 - daysAgo) / 3 % sampleNotes.Length];

                await _database.InsertAsync(new EntryNote
                {
                    Text = note,
                    DateTime = DateTime.Now.AddDays(-daysAgo)
                });
            }
        }
    }

}