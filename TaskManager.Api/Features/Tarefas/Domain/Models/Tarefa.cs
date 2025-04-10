using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Api.Core.Helpers.ExtensionMethods;
using TaskManager.Api.Core.Models;
using TaskManager.Api.Features.Tarefas.Domain.Constants;
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
            .HasMaxLength(LengthConstants.TITULO_MAX_LENGTH)
            .IsRequired();
    }
}

public class TarefaValidator : AbstractValidator<Tarefa>
{
    public TarefaValidator()
    {
        RuleFor(tarefa => tarefa.Titulo)
            .NotEmpty().WithMessage("Título deve ser preenchido.")
            .MaximumLength(LengthConstants.TITULO_MAX_LENGTH).WithMessage($"Título deve ter no máximo {LengthConstants.TITULO_MAX_LENGTH} caracteres.");

        When(tarefa => tarefa.DataConclusao is not null, () =>
        {
            RuleFor(tarefa => tarefa.DataConclusao)
                .Must((tarefa, dataConclusao) => dataConclusao > tarefa.DataConclusao)
                .WithMessage("Data de conclusão deve ser posterior à data de criação.");
        });

        RuleFor(tarefa => tarefa.Status)
            .IsInEnum().WithMessage("Status inválido.");
    }
}