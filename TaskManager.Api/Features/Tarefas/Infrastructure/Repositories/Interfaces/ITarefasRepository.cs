using TaskManager.Api.Core.Repositories.Interfaces;
using TaskManager.Api.Features.Tarefas.Domain.Models;

namespace TaskManager.Api.Features.Tarefas.Infrastructure.Repositories.Interfaces;

public interface ITarefasRepository : IBaseRepository<Tarefa>
{
}
