using Desafio.Domain.Common;

namespace Desafio.Domain.Entities;
public class ClientEntity : AuditableEntity
{
    public UserEntity? ClientUserEntity { get; set; }

    public IList<ServiceEntity>? ServicesList { get; set; }

    public string? ProfilePhoto { get; set; }

    public bool? ClientExcluded { get; set; }
}
