using MediatR;
using Desafio.Application.Services.Service.Queries.GetServiceById;
using Desafio.Domain.Enums;

namespace Desafio.Application.Services.Service.Commands.CreateService;
public class CreateServiceCommand : IRequest<ServiceDto>
{
    public PlansEnum Plan { get; set; }

    public DateTime? InitialDateTime { get; set; }

    public DateTime? FinalDateTime { get; set; }

    public DateTime? PrevisionFinalDateTime { get; set; }

    public Guid ClientId { get; set; }
}
