using TaskManager.Domain.Core.Models;

namespace TaskManager.Domain.Core.Repositories.Interfaces;

public interface IBaseRepository<T> where T : BaseModel
{
    public Task<List<T>> GetAllAsync();
    public Task<T?> GetByIdAsync(int id);
    public Task<int> CreateAsync(T model);
    public Task<bool> UpdateAsync(T model);
    public Task<bool> DeleteByIdAsync(int id);
}
