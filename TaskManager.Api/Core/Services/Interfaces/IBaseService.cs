using LanguageExt;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Core.Models;

namespace TaskManager.Api.Core.Services.Interfaces;

public interface IBaseService<T> where T : BaseModel
{
    public Task<Either<ProblemDetails, List<T>>> GetAllAsync();
    public Task<Either<ProblemDetails, T>> GetByIdAsync(int id);
    public Task<Either<ProblemDetails, int>> CreateAsync(T model);
    public Task<Either<ProblemDetails, Unit>> UpdateAsync(int id, T model);
    public Task<Either<ProblemDetails, Unit>> DeleteByIdAsync(int id);
}
