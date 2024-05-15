using Desafio.Domain.Common;

namespace Desafio.Domain.Entities;
public class UserTypeEntity : AuditableEntity
{
    public string? UserTypeName { get; set; }
}
