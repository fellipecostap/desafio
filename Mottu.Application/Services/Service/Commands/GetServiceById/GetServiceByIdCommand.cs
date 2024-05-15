using MediatR;
using Desafio.Application.Services.Service.Queries.GetServiceById;

namespace Desafio.Application.Services.Service.Commands.GetServiceById;
public class GetServiceByIdCommand : IRequest<ServiceDto>
{
    public Guid ServiceId { get; set; }
}
