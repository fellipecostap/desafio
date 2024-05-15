using Desafio.Application.Common.Mappings;
using Desafio.Domain.Entities;

namespace Desafio.Application.Services.Client.Queries.GetClientFilters;
public class ClientSimpleDto : IMapFrom<ClientEntity>
{
    public Guid ClientId { get; set; }

    public Guid UserId { get; set; }

    public string? Name { get; set; }

    public string? ProfilePhoto { get; set; }

    public bool? Favorite { get; set; }
}
