using LanguageExt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TaskManager.Api.Core.Services;
using TaskManager.Domain.Core.Models;
using TaskManager.Domain.Core.Repositories.Interfaces;

namespace TaskManager.Tests.Unit.Core;

public abstract class BaseServiceTests<T> where T : BaseModel
{
    private readonly Mock<IBaseRepository<T>> _mockRepo;
    private readonly BaseService<T> _service;

    protected BaseServiceTests(BaseService<T> service, Mock<IBaseRepository<T>> mockRepo)
    {
        _mockRepo = mockRepo;
        _service = service;
    }

    #region GetAllAsync

    [Fact]
    public async Task GetAllAsync_ShouldReturnList_WhenSuccessful()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<T>());

        // Act
        Either<ProblemDetails, List<T>> result = await _service.GetAllAsync();

        // Assert
        Assert.True(result.IsRight);
        Assert.NotNull(result.RightAsEnumerable().First());
    }

    #endregion

    #region GetByIdAsync

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNotFound_WhentNotInDataBase()
    {
        // Arrange
        int id = 1;

        _mockRepo.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync((T?)null);

        // Act
        Either<ProblemDetails, T> result = await _service.GetByIdAsync(id);

        // Assert
        Assert.True(result.IsLeft);
        Assert.Equal(StatusCodes.Status404NotFound, result.LeftAsEnumerable().First().Status);
        Assert.Equal($"Failed to get {_service.modelName} ID {id}.", result.LeftAsEnumerable().First().Title);
        Assert.Equal($"The requested {_service.modelName} was not found in the server.", result.LeftAsEnumerable().First().Detail);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnModel_WhenSuccessful()
    {
        // Arrange
        int id = 1;

        T modelInDataBase = Activator.CreateInstance<T>();

        modelInDataBase.Id = id;

        _mockRepo.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(modelInDataBase);

        // Act
        Either<ProblemDetails, T> result = await _service.GetByIdAsync(id);

        // Assert
        Assert.True(result.IsRight);
        Assert.Equal(modelInDataBase, result.RightAsEnumerable().First());
    }

    #endregion

    #region CreateAsync

    [Fact]
    public async Task CreateAsync_ShouldReturnId_WhenSuccessful()
    {
        // Arrange
        int createdId = 1;

        T modelToCreate = Activator.CreateInstance<T>();

        _mockRepo.Setup(repo => repo.CreateAsync(modelToCreate)).ReturnsAsync(createdId);

        // Act
        Either<ProblemDetails, int> result = await _service.CreateAsync(modelToCreate);

        // Assert
        Assert.True(result.IsRight);
        Assert.Equal(createdId, result.RightAsEnumerable().First());
        Assert.Equal(createdId, modelToCreate.Id);
    }

    #endregion

    #region UpdateAsync

    [Fact]
    public async Task UpdateAsync_ShouldReturnBadRequest_WhenRouteIdIsDifferentThanBodyId()
    {
        // Arrange
        int routeId = 1;

        T modelToUpdate = Activator.CreateInstance<T>();

        modelToUpdate.Id = 2;

        // Act
        Either<ProblemDetails, LanguageExt.Unit> result = await _service.UpdateAsync(routeId, modelToUpdate);

        // Assert
        Assert.True(result.IsLeft);
        Assert.Equal(StatusCodes.Status400BadRequest, result.LeftAsEnumerable().First().Status);
        Assert.Equal($"Failed to update {_service.modelName} ID {routeId}.", result.LeftAsEnumerable().First().Title);
        Assert.Equal("Body object ID and route ID are different.", result.LeftAsEnumerable().First().Detail);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnNotFound_WhentNotInDataBase()
    {
        // Arrange
        int routeId = 1;

        T modelToUpdate = Activator.CreateInstance<T>();

        modelToUpdate.Id = routeId;

        _mockRepo.Setup(repo => repo.GetByIdAsync(routeId)).ReturnsAsync((T?)null);

        // Act
        Either<ProblemDetails, LanguageExt.Unit> result = await _service.UpdateAsync(routeId, modelToUpdate);

        // Assert
        Assert.True(result.IsLeft);
        Assert.Equal(StatusCodes.Status404NotFound, result.LeftAsEnumerable().First().Status);
        Assert.Equal($"Failed to update {_service.modelName} ID {routeId}.", result.LeftAsEnumerable().First().Title);
        Assert.Equal($"The requested {_service.modelName} was not found in the server.", result.LeftAsEnumerable().First().Detail);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnUnit_WhenSuccessful()
    {
        // Arrange
        int routeId = 1;

        T modelInDataBase = Activator.CreateInstance<T>();

        modelInDataBase.Id = routeId;

        T modelToUpdate = Activator.CreateInstance<T>();

        modelToUpdate.Id = routeId;

        _mockRepo.Setup(repo => repo.GetByIdAsync(routeId)).ReturnsAsync(modelInDataBase);
        _mockRepo.Setup(repo => repo.UpdateAsync(modelToUpdate)).ReturnsAsync(true);

        // Act
        Either<ProblemDetails, LanguageExt.Unit> result = await _service.UpdateAsync(routeId, modelToUpdate);

        // Assert
        Assert.True(result.IsRight);
    }

    #endregion

    #region DeleteByIdAsync

    [Fact]
    public async Task DeleteByIdAsync_ShouldReturnNotFound_WhenNotInDataBase()
    {
        // Arrange
        int routeId = 1;

        _mockRepo.Setup(repo => repo.GetByIdAsync(routeId)).ReturnsAsync((T?)null);

        // Act
        Either<ProblemDetails, LanguageExt.Unit> result = await _service.DeleteByIdAsync(routeId);

        // Assert
        Assert.True(result.IsLeft);
        Assert.Equal(StatusCodes.Status404NotFound, result.LeftAsEnumerable().First().Status);
        Assert.Equal($"Failed to delete {_service.modelName} ID {routeId}.", result.LeftAsEnumerable().First().Title);
        Assert.Equal($"The requested {_service.modelName} was not found in the server.", result.LeftAsEnumerable().First().Detail);
    }

    [Fact]
    public async Task DeleteByIdAsync_ShouldReturnUnit_WhenSuccessful()
    {
        // Arrange
        int routeId = 1;

        T modelInDataBase = Activator.CreateInstance<T>();

        modelInDataBase.Id = routeId;

        _mockRepo.Setup(repo => repo.GetByIdAsync(routeId)).ReturnsAsync(modelInDataBase);
        _mockRepo.Setup(repo => repo.DeleteByIdAsync(routeId)).ReturnsAsync(true);

        // Act
        Either<ProblemDetails, LanguageExt.Unit> result = await _service.DeleteByIdAsync(routeId);

        // Assert
        Assert.True(result.IsRight);
    }

    #endregion
}
