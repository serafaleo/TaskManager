using Moq;
using TaskManager.Api.Features.Tarefas.Application.Services;
using TaskManager.Api.Features.Tarefas.Infrastructure.Repositories.Interfaces;
using TaskManager.Domain.Core.Repositories.Interfaces;
using TaskManager.Domain.Features.Tarefas.Models;
using TaskManager.Tests.Unit.Core;

namespace TaskManager.Tests.Unit.Features;

public class TarefasServiceTests : BaseServiceTests<Tarefa>
{
    private static readonly Mock<ITarefasRepository> _mockRepo = new Mock<ITarefasRepository>();

    public TarefasServiceTests() : base(new TarefasService(_mockRepo.Object), _mockRepo.As<IBaseRepository<Tarefa>>())
    {
    }
}
