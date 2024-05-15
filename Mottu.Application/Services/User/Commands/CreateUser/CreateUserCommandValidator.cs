using FluentValidation;
using Desafio.Application.Common.Functions;
using Desafio.Application.Resources;

namespace Desafio.Application.Services.User.Commands.CreateUser;
public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(s => s.UserCNH)
         .CnpjValid()
         .NotEmpty().WithMessage(x => GlobalMessages.Field_Invalid);

        RuleFor(s => s.UserType)
         .NotEmpty().WithMessage(x => GlobalMessages.Field_CannotEmpty);

        RuleFor(s => s.UserMail)
         .EmailAddress().WithMessage(x => GlobalMessages.InvalidEmail);

        RuleFor(v => v.Password)
         .NotEmpty().WithMessage(x => GlobalMessages.PreRegistration_Password_CannotEmpty)

         .MinimumLength(8).WithMessage(x => GlobalMessages.PreRegistration_Password_MinimumLenght)

         .Matches("(?=.*[a-z])").WithMessage(x => GlobalMessages.PreRegistration_Password_LowerCaseLetter)

         .Matches("(?=.*[A-Z])").WithMessage(x => GlobalMessages.PreRegistration_Password_CapitalLetter)

         .Matches("(?=.*[0-9])").WithMessage(x => GlobalMessages.PreRegistration_Password_LeastOneNumber)

         .Matches("[^A-Za-z0-9a-áàâãéèêíïóôõöúçñ]").WithMessage(x => GlobalMessages.PreRegistration_Password_SpecialChar);

        RuleFor(s => s.UserName)
         .NotEmpty().WithMessage(x => GlobalMessages.Field_CannotEmpty);
        RuleFor(s => s.UserCNPJ)
         .NotEmpty().WithMessage(x => GlobalMessages.Field_CannotEmpty);
        RuleFor(s => s.BirthDate)
         .NotEmpty().WithMessage(x => GlobalMessages.Field_CannotEmpty);
        RuleFor(s => s.CNHPhoto)
         .NotEmpty().WithMessage(x => GlobalMessages.Field_CannotEmpty);
        RuleFor(s => s.CNHType)
         .NotEmpty().WithMessage(x => GlobalMessages.Field_CannotEmpty);
        RuleFor(s => s.UserCNH)
         .NotEmpty().WithMessage(x => GlobalMessages.Field_CannotEmpty);
    }
}
