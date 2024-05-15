using MediatR;

namespace Desafio.Application.Services.SendEmail.Commands.SendEmail.PreRegistration;
public class SendEmailPreRegistrationCommand : IRequest<Guid>
{
    public string? UserEmail { get; set; }

    public string? UserToken { get; set; }

    public string? NickName { get; set; }
}
