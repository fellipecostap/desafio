using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Desafio.Application.Services.Login.Commands.ChangePassword;
using Desafio.Application.Services.Login.Commands.ForgotPassword;
using Desafio.Application.Services.Login.Commands.LoginUser;
using Desafio.Application.Services.Login.Commands.RefreshToken;
using Desafio.Application.Services.Login.Commands.ValidatePassord;
using Desafio.Application.Services.Login.Queries.LoginUser;

namespace Desafio.WebApi.Controllers;

public class LoginController : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<LoginDto>> Post(LoginUserCommand command)
    {
        return await Mediator.Send(command);
    }
}
