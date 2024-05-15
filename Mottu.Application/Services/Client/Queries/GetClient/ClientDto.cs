using Desafio.Application.Common.Mappings;
using Desafio.Application.Services.Service.Queries.GetServiceById;
using Desafio.Application.Services.User.Queries.GetUser;
using Desafio.Domain.Entities;

namespace Desafio.Application.Services.Client.Queries.GetClient;
public class ClientDto : IMapFrom<ClientEntity>
{
    public Guid Id { get; set; }

    public UserDto? ClientUserEntity { get; set; }

    public IList<ServiceDto>? ServicesList { get; set; }

    public string? ProfilePhoto { get; set; }

    public bool? ClientExcluded { get; set; }
}
