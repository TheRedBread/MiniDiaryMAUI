namespace MiniDiaryMAUI.Services;

public interface IDatabaseService
{
    Task InitializeAsync();

    Task<List<T>> GetAllAsync<T>() where T : new();

    Task<int> InsertAsync<T>(T entity) where T : new();

    Task<int> UpdateAsync<T>(T entity) where T : new();

    Task<int> DeleteAsync<T>(T entity) where T : new();

    Task<T> GetByIdAsync<T>(int id) where T : new();
}
