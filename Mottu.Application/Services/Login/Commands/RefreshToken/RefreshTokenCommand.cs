using MediatR;
using Desafio.Application.Services.Login.Queries.LoginUser;

namespace Desafio.Application.Services.Login.Commands.RefreshToken;
public class RefreshTokenCommand : IRequest<LoginDto>
{
    public string? Token { get; set; }

    public string? RefreshToken { get; set; }
}
