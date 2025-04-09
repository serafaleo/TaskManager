using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using TaskManager.Api.Core.Middlewares;
using TaskManager.Api.Core.Services.Interfaces;
using TaskManager.Api.Core.Services;
using Scalar.AspNetCore;
using TaskManager.Api.Features.Tarefas.Application.Services.Interfaces;
using TaskManager.Api.Features.Tarefas.Application.Services;
using TaskManager.Api.Features.Tarefas.Infrastructure.Repositories.Interfaces;
using TaskManager.Api.Features.Tarefas.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

#region Core
builder.Services.AddSingleton<IDbConnectionService, DbConnectionService>();
#endregion

#region Tarefas
builder.Services.AddScoped<ITarefasService, TarefasService>();
builder.Services.AddScoped<ITarefasRepository, TarefasRepository>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
