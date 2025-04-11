using System.ComponentModel;

namespace TaskManager.Domain.Features.Tarefas.Enums;

public enum EStatus
{
    [Description("Pendente")]
    Pendente,

    [Description("Em Progresso")]
    EmProgresso,

    [Description("Concluída")]
    Concluida
}
