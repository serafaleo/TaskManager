using TaskManager.Domain.Core.Repositories.Interfaces;
using TaskManager.Domain.Features.Tarefas.Models;

namespace TaskManager.Api.Features.Tarefas.Infrastructure.Repositories.Interfaces;

public interface ITarefasRepository : IBaseRepository<Tarefa>
{
}
