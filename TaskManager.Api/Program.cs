using FluentValidation;
using FluentValidation.AspNetCore;
using Scalar.AspNetCore;
using TaskManager.Api.Core.Middlewares;
using TaskManager.Api.Core.Services;
using TaskManager.Api.Core.Services.Interfaces;
using TaskManager.Api.Features.Tarefas.Application.Services;
using TaskManager.Api.Features.Tarefas.Application.Services.Interfaces;
using TaskManager.Api.Features.Tarefas.Infrastructure.Repositories;
using TaskManager.Api.Features.Tarefas.Infrastructure.Repositories.Interfaces;
using TaskManager.Domain.Core.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssembly(typeof(BaseModel).Assembly);

#region Core
builder.Services.AddSingleton<IDbConnectionService, DbConnectionService>();
#endregion

#region Tarefas
builder.Services.AddScoped<ITarefasService, TarefasService>();
builder.Services.AddScoped<ITarefasRepository, TarefasRepository>();
#endregion

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        policy =>
        {
            policy.WithOrigins("https://localhost:44395") // frontend origin
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseCors("AllowLocalhost");

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
