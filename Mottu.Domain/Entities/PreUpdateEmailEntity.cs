using Desafio.Domain.Common;

namespace Desafio.Domain.Entities;
public class PreUpdateEmailEntity : AuditableEntity
{
    public string OldEmail { get; set; }
    public string NewEmail { get; set; }
    public string TokenValidation { get; set; }
    public UserEntity? UserEntity { get; set; }
}
