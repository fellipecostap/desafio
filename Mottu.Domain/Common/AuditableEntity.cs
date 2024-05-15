namespace Desafio.Domain.Common;

public abstract class AuditableEntity
{
    public Guid Id { get; set; }
    public string? Created { get; set; }
    public string? CreatedBy { get; set; }
    public string? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
}
