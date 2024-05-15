using Desafio.Application.Common.Mappings;
using Desafio.Domain.Entities;

namespace Desafio.Application.Services.Client.Queries.GetClient;
public class ClientPhotoDto : IMapFrom<ClientEntity>
{
    public Guid ClientId { get; set; }
    public string? ProfilePhoto { get; set; }
}
