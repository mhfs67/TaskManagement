using TaskManagement.API.DTOs;
using FluentValidation;

namespace TaskManagement.API.Validation
{
    public class CreateCommentValidator : AbstractValidator<CreateCommentDTO>
    {
        public CreateCommentValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty()
                .MaximumLength(500)
                .WithMessage("O conteúdo do comentário é obrigatório e não deve exceder 500 caracteres");
        }
    }
}
