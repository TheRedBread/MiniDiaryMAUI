using MiniDiaryMAUI.Models;
using SQLite;

namespace MiniDiaryMAUI.Services;

public class DatabaseService : IDatabaseService
{
    private readonly SQLiteAsyncConnection _database;

    public DatabaseService()
    {
        var databasePath = Path.Combine(
            FileSystem.AppDataDirectory,
            "minidiary.db3");

        _database = new SQLiteAsyncConnection(databasePath);
    }

    public async Task InitializeAsync()
    {
        await _database.CreateTableAsync<EntryNote>();
        await _database.CreateTableAsync<EntryWeight>();
    }

    public Task<List<T>> GetAllAsync<T>() where T : new()
    {
        return _database
            .Table<T>()
            .ToListAsync();
    }

    public Task<int> InsertAsync<T>(T entity) where T : new()
    {
        return _database.InsertAsync(entity);
    }

    public Task<int> UpdateAsync<T>(T entity) where T : new()
    {
        return _database.UpdateAsync(entity);
    }

    public Task<int> DeleteAsync<T>(T entity) where T : new()
    {
        return _database.DeleteAsync(entity);
    }
}
