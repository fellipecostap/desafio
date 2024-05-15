using Desafio.Application.Services.Client.Queries.GetClient;
using MediatR;

namespace Desafio.Application.Services.Motorcycle.Commands.UpdateMotorcycle;
public class UpdateMotorcycleCommand : IRequest<MotorcycleDto>
{
    public Guid? ClientId { get; set; }
    public string? Identifier { get; set; }
    public string? Year { get; set; }
    public string? Model { get; set; }
    public string? Plate { get; set; }
}
