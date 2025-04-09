using LanguageExt;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Core.Models;
using TaskManager.Api.Core.Repositories.Interfaces;
using TaskManager.Api.Core.Services.Interfaces;
using TaskManager.Api.Core.Helpers.ExtensionMethods;

namespace TaskManager.Api.Core.Services;

public abstract class BaseService<T>(IBaseRepository<T> repo) : IBaseService<T> where T : BaseModel
{
    private readonly string _modelName = $"{typeof(T).Name}";
    private readonly IBaseRepository<T> _repo = repo;

    public async Task<Either<ProblemDetails, List<T>>> GetAllAsync()
    {
        return await _repo.GetAllAsync();
    }

    public async Task<Either<ProblemDetails, T>> GetByIdAsync(int id)
    {
        Either<ProblemDetails, T> validation = await ValidateModelInDatabase(id, "get");

        if (validation.IsLeft)
        {
            return validation.LeftAsEnumerable().First();
        }

        return validation.RightAsEnumerable().First();
    }

    public async Task<Either<ProblemDetails, int>> CreateAsync(T model)
    {
        model.Id = await _repo.CreateAsync(model);
        return model.Id;
    }

    public async Task<Either<ProblemDetails, Unit>> UpdateAsync(int id, T model)
    {
        const string action = "update";

        if (model.Id != default && model.Id != id)
        {
            return new ProblemDetails().BadRequest(BuildDefaultErrorTitle(id, action), "Body object ID and route ID are different.");
        }

        Either<ProblemDetails, T> validation = await ValidateModelInDatabase(id, action);

        if (validation.IsLeft)
        {
            return validation.LeftAsEnumerable().First();
        }

        await _repo.UpdateAsync(model);
        return new Unit();
    }

    public async Task<Either<ProblemDetails, Unit>> DeleteByIdAsync(int id)
    {
        Either<ProblemDetails, T> validation = await ValidateModelInDatabase(id, "delete");

        if (validation.IsLeft)
        {
            return validation.LeftAsEnumerable().First();
        }

        await _repo.DeleteByIdAsync(id);
        return new Unit();
    }

    private async Task<Either<ProblemDetails, T>> ValidateModelInDatabase(int id, string action)
    {
        T? modelInDataBase = await _repo.GetByIdAsync(id);
        string defaultErrorTitle = BuildDefaultErrorTitle(id, action);

        if (modelInDataBase is null)
        {
            return new ProblemDetails().DefaultNotFound(defaultErrorTitle, _modelName);
        }

        return modelInDataBase;
    }

    private string BuildDefaultErrorTitle(int id, string action)
    {
        return $"Failed to {action} {_modelName} ID {id}.";
    }
}
