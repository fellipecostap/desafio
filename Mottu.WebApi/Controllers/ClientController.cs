using Desafio.Application.Services.Client.Commands.CreateClient;
using Desafio.Application.Services.Client.Commands.UpdateClient;
using Desafio.Application.Services.Client.Queries.GetClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.WebApi.Controllers;

public class ClientController : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<ClientDto>> Post(CreateClientCommand command)
    {
        return await Mediator.Send(command);
    }

    [Authorize("All")]
    [HttpPut("UpdatePhoto")]
    public async Task<ActionResult<ClientPhotoDto>> Put(UpdatePhotoClientCommand command)
    {
        return await Mediator.Send(command);
    }
}
