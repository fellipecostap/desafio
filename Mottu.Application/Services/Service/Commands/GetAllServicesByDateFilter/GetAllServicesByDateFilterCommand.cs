using MediatR;
using Desafio.Application.Services.Service.Queries.GetServiceById;

namespace Desafio.Application.Services.Service.Commands.GetAllServicesByDateFilter;
public class GetAllServicesByDateFilterCommand : IRequest<ServiceVm>
{
    public Guid CompanyEntityId { get; set; }
    public bool CurrentWeek { get; set; }
    public bool CurrentMonth { get; set; }
    public DateTime? ServiceDayScheduled { get; set; }
}
