using MediatR;

namespace Desafio.Application.Services.PreRegistration.Commands.ResendToken;
public class ResendTokenCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
}
