using TaskManager.Api.Core.Services;
using TaskManager.Api.Features.Tarefas.Application.Services.Interfaces;
using TaskManager.Api.Features.Tarefas.Domain.Models;
using TaskManager.Api.Features.Tarefas.Infrastructure.Repositories.Interfaces;

namespace TaskManager.Api.Features.Tarefas.Application.Services;

public class TarefasService(ITarefasRepository repo) : BaseService<Tarefa>(repo), ITarefasService
{
}
