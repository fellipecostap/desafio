using Microsoft.AspNetCore.Mvc;
using Desafio.Application.Services.PreRegistration.Commands.ConfirmToken;

namespace Desafio.WebApi.Controllers;

public class ConfirmTokenController : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult> Create(ConfirmTokenCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
}
