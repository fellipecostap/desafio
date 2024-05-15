using Desafio.Application.Resources;
using FluentValidation;

namespace Desafio.Application.Services.Client.Commands.CreateClient;
public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
{
    public CreateClientCommandValidator()
    {
        RuleFor(s => s.ProfilePhoto)
         .NotEmpty().WithMessage(x => GlobalMessages.Field_CannotEmpty);
        RuleFor(s => s.ClientUser)
         .NotEmpty().WithMessage(x => GlobalMessages.Field_CannotEmpty);
    }
}