using FluentValidation;
using TaskManager.Domain.Core.Models;
using TaskManager.Domain.Features.Tarefas.Constants;
using TaskManager.Domain.Features.Tarefas.Enums;

namespace TaskManager.Domain.Features.Tarefas.Models;

public class Tarefa : BaseModel
{
    public string? Titulo { get; set; }
    public string? Descricao { get; set; }
    public DateTime DataCriacao { get; set; } = DateTime.Now;
    public DateTime? DataConclusao { get; set; }
    public EStatus Status { get; set; }

    public Tarefa Clone()
    {
        return (Tarefa)this.MemberwiseClone();
    }
}

public class TarefaValidator : AbstractValidator<Tarefa>
{
    public TarefaValidator()
    {
        RuleFor(tarefa => tarefa.Titulo)
            .NotEmpty().WithMessage("Título deve ser preenchido.")
            .MaximumLength(LengthConstants.TITULO_MAX_LENGTH).WithMessage($"Título deve ter no máximo {LengthConstants.TITULO_MAX_LENGTH} caracteres.");

        When(tarefa => tarefa.DataConclusao.HasValue, () =>
        {
            RuleFor(tarefa => tarefa.DataConclusao!.Value)
                .GreaterThan(tarefa => tarefa.DataCriacao)
                .WithMessage("Data de conclusão deve ser posterior à data de criação.");
        });

        When(tarefa => tarefa.Status == EStatus.Concluida, () =>
        {
            RuleFor(tarefa => tarefa.DataConclusao)
                .NotEmpty().WithMessage("Data de conslusão é obrigatória quando o status for Concluída.");
        });

        RuleFor(tarefa => tarefa.Status)
            .IsInEnum().WithMessage("Status inválido.");
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<Tarefa>.CreateWithOptions((Tarefa)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}