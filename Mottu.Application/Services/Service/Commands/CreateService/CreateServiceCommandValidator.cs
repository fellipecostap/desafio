using Desafio.Application.Resources;
using FluentValidation;

namespace Desafio.Application.Services.Service.Commands.CreateService;
public class CreateServiceCommandValidator : AbstractValidator<CreateServiceCommand>
{
    public CreateServiceCommandValidator()
    {
        RuleFor(s => s.InitialDateTime)
         .NotEmpty().WithMessage(x => GlobalMessages.Field_CannotEmpty);
        RuleFor(s => s.FinalDateTime)
         .NotEmpty().WithMessage(x => GlobalMessages.Field_CannotEmpty);
        RuleFor(s => s.PrevisionFinalDateTime)
         .NotEmpty().WithMessage(x => GlobalMessages.Field_CannotEmpty);
    }
}
