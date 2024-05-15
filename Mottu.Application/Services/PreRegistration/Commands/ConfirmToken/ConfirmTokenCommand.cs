using MediatR;

namespace Desafio.Application.Services.PreRegistration.Commands.ConfirmToken;
public class ConfirmTokenCommand : IRequest
{
    public Guid Id { get; set; }

    public string? Token { get; set; }

}
