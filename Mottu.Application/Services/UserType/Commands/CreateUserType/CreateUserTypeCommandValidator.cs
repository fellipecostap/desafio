using FluentValidation;
using Desafio.Application.Resources;

namespace Desafio.Application.Services.UserType.Commands.CreateUserType;
public class CreateUserTypeCommandValidator : AbstractValidator<CreateUserTypeCommand>
{
    public CreateUserTypeCommandValidator()
    {
    }
}
