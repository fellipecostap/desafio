using FluentValidation;
using Desafio.Application.Resources;

namespace Desafio.Application.Services.SendEmail.Commands.SendEmail.PreRegistration;
public class SendEmailPreRegistrationCommandValidator : AbstractValidator<SendEmailPreRegistrationCommand>
{
    public SendEmailPreRegistrationCommandValidator()
    {
        RuleFor(v => v.UserEmail)
            .NotEmpty().WithMessage(x => GlobalMessages.SendMail_UserEmail_CannotEmpty);
    }
}
