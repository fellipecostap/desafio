using MediatR;

namespace Desafio.Application.Services.Login.Commands.ForgotPassword;
public class ForgotPasswordCommand : IRequest<Guid>
{
    public string? Parameter { get; set; }

    public Guid? UserType { get; set; }
}
