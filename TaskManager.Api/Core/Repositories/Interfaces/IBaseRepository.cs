using TaskManager.Api.Core.Models;

namespace TaskManager.Api.Core.Repositories.Interfaces;

public interface IBaseRepository<T> where T : BaseModel
{
    public Task<List<T>> GetAllAsync();
    public Task<T?> GetByIdAsync(int id);
    public Task<int> CreateAsync(T model);
    public Task<bool> UpdateAsync(T model);
    public Task<bool> DeleteByIdAsync(int id);
}
