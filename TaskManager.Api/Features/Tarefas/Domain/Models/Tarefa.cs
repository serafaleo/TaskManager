using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Api.Core.Helpers.ExtensionMethods;
using TaskManager.Api.Core.Models;
using TaskManager.Api.Features.Tarefas.Domain.Enums;

namespace TaskManager.Api.Features.Tarefas.Domain.Models;

public class Tarefa : BaseModel
{
    public required string Titulo { get; set; }
    public string? Descricao { get; set; }
    public DateTime DataCriacao { get; set; } = DateTime.Now;
    public DateTime? DataConclusao { get; set; }
    public EStatus Status { get; set; }
}

public class TarefaConfiguration : IEntityTypeConfiguration<Tarefa>
{
    public void Configure(EntityTypeBuilder<Tarefa> builder)
    {
        builder.BaseConfiguration();

        builder.Property(tarefa => tarefa.Titulo)
            .HasMaxLength(100)
            .IsRequired();
    }
}