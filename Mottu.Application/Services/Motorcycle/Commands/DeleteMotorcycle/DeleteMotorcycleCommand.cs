using MediatR;

namespace Desafio.Application.Services.Motorcycle.Commands.DeleteMotorcycle;
public class DeleteMotorcycleCommand : IRequest
{
    public Guid? MotorcycleId { get; set; }
}
