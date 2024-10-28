using TaskManagement.API.DTOs;
using FluentValidation;

namespace TaskManagement.API.Validation
{
    public class CreateProjectValidator : AbstractValidator<CreateProjectDTO>
    {
        public CreateProjectValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("O nome do projeto é obrigatório e não deve exceder 100 caracteres");

            RuleFor(x => x.Description)
                .MaximumLength(500)
                .WithMessage("A descrição do projeto não deve exceder 500 caracteres");
        }
    }
}
