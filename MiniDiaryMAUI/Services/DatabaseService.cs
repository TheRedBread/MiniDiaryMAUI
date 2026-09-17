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
            await _database.InsertAsync(new EntryWeight
            {
                Weight = 82.5,
                DateTime = DateTime.Now.AddDays(-2)
            });

            await _database.InsertAsync(new EntryWeight
            {
                Weight = 81.9,
                DateTime = DateTime.Now.AddDays(-1)
            });

            await _database.InsertAsync(new EntryWeight
            {
                Weight = 81.4,
                DateTime = DateTime.Now
            });
        }

        var notes = await _database.Table<EntryNote>().ToListAsync();

        if (notes.Count == 0)
        {
            await _database.InsertAsync(new EntryNote
            {
                Text = "Started tracking my weight.",
                DateTime = DateTime.Now.AddDays(-2)
            });

            await _database.InsertAsync(new EntryNote
            {
                Text = "Feeling good today.",
                DateTime = DateTime.Now.AddDays(-1)
            });

            await _database.InsertAsync(new EntryNote
            {
                Text = "Went for a long walk.",
                DateTime = DateTime.Now
            });
        }
    }

}