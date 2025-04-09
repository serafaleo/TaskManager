using TaskManager.Api.Core.Repositories;
using TaskManager.Api.Core.Services.Interfaces;
using TaskManager.Api.Features.Tarefas.Domain.Models;
using TaskManager.Api.Features.Tarefas.Infrastructure.Repositories.Interfaces;

namespace TaskManager.Api.Features.Tarefas.Infrastructure.Repositories;

public class TarefasRepository(IDbConnectionService db) : BaseRepository<Tarefa>(db), ITarefasRepository
{
}
