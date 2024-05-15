using Desafio.Application.Common.Mappings;
using Desafio.Application.Services.Service.Queries.GetServiceById;
using Desafio.Application.Services.User.Queries.GetUser;
using Desafio.Domain.Entities;

namespace Desafio.Application.Services.Client.Queries.GetClient;
public class MotorcycleDto : IMapFrom<MotorcycleEntity>
{
    public Guid Id { get; set; }
    public ClientDto Client { get; set; }
    public string? Identifier { get; set; }
    public string? Year { get; set; }
    public string? Model { get; set; }
    public string? Plate { get; set; }
    public IList<ServiceDto>? Services { get; set; }
}
