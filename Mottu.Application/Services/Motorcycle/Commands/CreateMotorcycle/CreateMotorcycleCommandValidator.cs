using Desafio.Application.Resources;
using Desafio.Application.Services.Client.Commands.CreateClient;
using FluentValidation;

namespace Desafio.Application.Services.Motorcycle.Commands.CreateMotorcycle;
public class CreateMotorcycleCommandValidator : AbstractValidator<CreateMotorcycleCommand>
{
    public CreateMotorcycleCommandValidator()
    {
        RuleFor(b => b.Identifier)
           .NotEmpty().WithMessage(x => GlobalMessages.Address_State_CannotEmpty);

        RuleFor(b => b.Year)
           .NotEmpty().WithMessage(x => GlobalMessages.Address_AdNumber_CannotEmpty);

        RuleFor(b => b.Model)
           .NotEmpty().WithMessage(x => GlobalMessages.Address_NeighborhoodName_CannotEmpty);

        RuleFor(b => b.Plate)
           .NotEmpty().WithMessage(x => GlobalMessages.Address_NeighborhoodName_CannotEmpty);
    }
}
