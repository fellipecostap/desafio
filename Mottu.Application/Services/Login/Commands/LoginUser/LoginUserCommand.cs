using MediatR;
using Desafio.Application.Services.Login.Queries.LoginUser;

namespace Desafio.Application.Services.Login.Commands.LoginUser;
public class LoginUserCommand : IRequest<LoginDto>
{
    public string? Email { get; set; }

    public string? Password { get; set; }
}
