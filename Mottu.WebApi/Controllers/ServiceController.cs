using Desafio.Application.Services.Service.Commands.CreateService;
using Desafio.Application.Services.Service.Commands.GetAllServicesByDateFilter;
using Desafio.Application.Services.Service.Commands.GetServiceById;
using Desafio.Application.Services.Service.Queries.GetServiceById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.WebApi.Controllers;
public class ServiceController : ApiControllerBase
{
    [Authorize("All")]
    [HttpPost()]
    public async Task<ActionResult<ServiceDto>> Post(CreateServiceCommand command)
    {
        return await Mediator.Send(command);
    }
}
