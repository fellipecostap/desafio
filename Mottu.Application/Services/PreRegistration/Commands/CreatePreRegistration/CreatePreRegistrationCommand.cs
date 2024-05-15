using MediatR;

namespace Desafio.Application.Services.PreRegistration.Commands.CreatePreRegistration;
public class CreatePreRegistrationCommand : IRequest<Guid>
{
    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? NickName { get; set; }
}
