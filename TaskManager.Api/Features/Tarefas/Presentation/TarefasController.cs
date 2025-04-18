﻿using TaskManager.Api.Core.Controllers;
using TaskManager.Api.Features.Tarefas.Application.Services.Interfaces;
using TaskManager.Domain.Features.Tarefas.Models;

namespace TaskManager.Api.Features.Tarefas.Presentation;

public class TarefasController(ITarefasService service) : BaseController<Tarefa>(service)
{
}
