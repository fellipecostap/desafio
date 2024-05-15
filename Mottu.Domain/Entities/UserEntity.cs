using Desafio.Domain.Common;
using Desafio.Domain.Entities;
using Desafio.Domain.Enums;

namespace Desafio.Domain.Entities;
public class UserEntity : AuditableEntity
{
    public string? Password { get; set; }

    public CNHTypeEnum CNHType { get; set; }

    public string? UserCNH { get; set; } // Unico

    public string? CNHPhoto { get; set; }

    public string? UserCnpj { get; set; } // Unico

    public string? UserName { get; set; }

    public string? UserMail { get; set; }

    public DateTime? BirthDate { get; set; }

    public bool? UserExcluded { get; set; }

    public UserTypeEntity? UserTypeEntity { get; set; }
}
