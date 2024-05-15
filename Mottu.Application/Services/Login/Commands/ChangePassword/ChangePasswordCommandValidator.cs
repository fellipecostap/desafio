using FluentValidation;
using Desafio.Application.Resources;

namespace Desafio.Application.Services.Login.Commands.ChangePassword;
public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(v => v.NewPassword)
         .NotEmpty().WithMessage(x => GlobalMessages.PreRegistration_Password_CannotEmpty)
         .MinimumLength(8).WithMessage(x => GlobalMessages.PreRegistration_Password_MinimumLenght)
         .Matches("(?=.*[a-z])").WithMessage(x => GlobalMessages.PreRegistration_Password_LowerCaseLetter)
         .Matches("(?=.*[A-Z])").WithMessage(x => GlobalMessages.PreRegistration_Password_CapitalLetter)
         .Matches("(?=.*[0-9])").WithMessage(x => GlobalMessages.PreRegistration_Password_LeastOneNumber)
         .Matches("[^A-Za-z0-9a-áàâãéèêíïóôõöúçñ]").WithMessage(x => GlobalMessages.PreRegistration_Password_SpecialChar);
    }
}
