using Desafio.Domain.Common;

namespace Desafio.Domain.Entities;
public class RefreshTokenEntity : AuditableEntity
{
    public UserEntity UserEntity { get; set; }

    public string? RefreshToken { get; set; }
}
