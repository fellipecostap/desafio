using MediatR;
using Desafio.Application.Common.Models;
using Desafio.Application.Services.Client.Queries.GetClient;
using Desafio.Application.Services.User.Commands.CreateUser;

namespace Desafio.Application.Services.Motorcycle.Commands.CreateMotorcycle;
public class CreateMotorcycleCommand : IRequest<MotorcycleDto>
{
    public Guid? ClientId { get; set; }
    public string? Identifier { get; set; }
    public string? Year { get; set; }
    public string? Model { get; set; }
    public string? Plate { get; set; }
}
