using TaskManager.Api.Core.Repositories;
using TaskManager.Api.Core.Services.Interfaces;
using TaskManager.Api.Features.Tarefas.Infrastructure.Repositories.Interfaces;
using TaskManager.Domain.Features.Tarefas.Models;

namespace TaskManager.Api.Features.Tarefas.Infrastructure.Repositories;

public class TarefasRepository(IDbConnectionService db) : BaseRepository<Tarefa>(db), ITarefasRepository
{
}
