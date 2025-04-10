using FluentValidation;
using TaskManager.Domain.Core.Models;
using TaskManager.Domain.Features.Tarefas.Constants;
using TaskManager.Domain.Features.Tarefas.Enums;

namespace TaskManager.Domain.Features.Tarefas.Models;

public class Tarefa : BaseModel
{
    public required string Titulo { get; set; }
    public string? Descricao { get; set; }
    public DateTime DataCriacao { get; set; } = DateTime.Now;
    public DateTime? DataConclusao { get; set; }
    public EStatus Status { get; set; }
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