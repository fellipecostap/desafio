using Microsoft.AspNetCore.Mvc;
using Desafio.Application.Services.PreRegistration.Commands.CreatePreRegistration;

namespace Desafio.WebApi.Controllers;

public class PreRegistrationController : ApiControllerBase
{

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreatePreRegistrationCommand command)
    {
        return await Mediator.Send(command);
    }
}
