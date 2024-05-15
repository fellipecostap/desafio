using MediatR;
using Desafio.Application.Services.Client.Queries.GetClient;

namespace Desafio.Application.Services.Motorcycle.Commands.GetMotorcycleByFilter;
public class GetAllMotorcycleByFilterCommand : IRequest<MotorcycleVm>
{
    public string Plate { get; set; }
}
