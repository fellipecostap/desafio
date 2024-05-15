using Desafio.Application.Services.Client.Queries.GetClient;
using Desafio.Application.Services.Motorcycle.Commands.CreateMotorcycle;
using Desafio.Application.Services.Motorcycle.Commands.DeleteMotorcycle;
using Desafio.Application.Services.Motorcycle.Commands.GetMotorcycleByFilter;
using Desafio.Application.Services.Motorcycle.Commands.UpdateMotorcycle;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.WebApi.Controllers;

public class MotorcycleController : ApiControllerBase
{
    [Authorize("Admin")]
    [HttpPost]
    public async Task<ActionResult<MotorcycleDto>> Create(CreateMotorcycleCommand command)
    {
        return await Mediator.Send(command);
    }

    [Authorize("Admin")]
    [HttpGet("GetAllMotorcycleByFilter")]
    public async Task<ActionResult<MotorcycleVm>> GetAllMotorcycleByFilter(string? plate)
    {
        return await Mediator.Send(new GetAllMotorcycleByFilterCommand()
        {
            Plate = plate
        });
    }

    [Authorize("Admin")]
    [HttpPut()]
    public async Task<ActionResult<MotorcycleDto>> Put(UpdateMotorcycleCommand command)
    {
        return await Mediator.Send(command);
    }

    [Authorize("Admin")]
    [HttpDelete()]
    public async Task<ActionResult> Delete(Guid motorcycleId)
    {
        await Mediator.Send(new DeleteMotorcycleCommand() { MotorcycleId = motorcycleId });
        return NoContent();
    }
}
