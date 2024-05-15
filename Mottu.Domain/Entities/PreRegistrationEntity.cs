using Desafio.Domain.Common;

namespace Desafio.Domain.Entities;
public class PreRegistrationEntity : AuditableEntity
{
    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? TokenValidation { get; set; }

    public string? NickName { get; set; }

    public bool TagValidation { get; set; } = false;

}
