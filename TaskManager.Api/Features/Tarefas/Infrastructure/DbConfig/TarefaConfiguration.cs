using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Api.Core.Helpers.ExtensionMethods;
using TaskManager.Domain.Features.Tarefas.Constants;
using TaskManager.Domain.Features.Tarefas.Models;

namespace TaskManager.Api.Features.Tarefas.Infrastructure.DbConfig;

public class TarefaConfiguration : IEntityTypeConfiguration<Tarefa>
{
    public void Configure(EntityTypeBuilder<Tarefa> builder)
    {
        builder.BaseConfiguration();

        builder.Property(tarefa => tarefa.Titulo)
            .HasMaxLength(LengthConstants.TITULO_MAX_LENGTH)
            .IsRequired();
    }
}