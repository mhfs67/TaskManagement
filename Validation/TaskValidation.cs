using TaskManagement.API.DTOs;
using FluentValidation;

namespace TaskManagement.API.Validation
{
    public class CreateTaskValidator : AbstractValidator<CreateTaskDTO>
    {
        public CreateTaskValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(200)
                .WithMessage("O título da tarefa é obrigatório e não deve exceder 200 caracteres");

            RuleFor(x => x.Description)
                .MaximumLength(1000)
                .WithMessage("A descrição da tarefa não deve exceder 1000 caracteres");

            RuleFor(x => x.DueDate)
                .NotEmpty()
                .Must(date => date > DateTime.UtcNow)
                .WithMessage("A data de vencimento deve ser no futuro");

            RuleFor(x => x.Priority)
                .NotEmpty()
                .Must(priority => Enum.TryParse<TaskPriority>(priority, out _))
                .WithMessage("Prioridade de tarefa inválida. Os valores válidos são: Baixo, Médio, Alto");
        }
    }

    public class UpdateTaskValidator : AbstractValidator<UpdateTaskDTO>
    {
        public UpdateTaskValidator()
        {
            RuleFor(x => x.Title)
                .MaximumLength(200)
                .When(x => x.Title != null)
                .WithMessage("O título da tarefa não deve exceder 200 caracteres");

            RuleFor(x => x.Description)
                .MaximumLength(1000)
                .When(x => x.Description != null)
                .WithMessage("A descrição da tarefa não deve exceder 1000 caracteres");

            RuleFor(x => x.DueDate)
                .Must(date => date == null || date > DateTime.UtcNow)
                .WithMessage("A data de vencimento deve ser no futuro");

            RuleFor(x => x.Status)
                .Must(status => status == null || Enum.TryParse<TaskStatus>(status, out _))
                .WithMessage("Status de tarefa inválido. Os valores válidos são: Pendente, Em andamento, Concluído");
        }
    }
}
