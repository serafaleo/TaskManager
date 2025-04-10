using TaskManager.Api.Core.Services;
using TaskManager.Api.Features.Tarefas.Application.Services.Interfaces;
using TaskManager.Api.Features.Tarefas.Infrastructure.Repositories.Interfaces;
using TaskManager.Domain.Features.Tarefas.Models;

namespace TaskManager.Api.Features.Tarefas.Application.Services;

public class TarefasService(ITarefasRepository repo) : BaseService<Tarefa>(repo), ITarefasService
{
}
