using Desafio.Domain.Common;
namespace Desafio.Domain.Entities;

public class MotorcycleEntity : AuditableEntity
{
    public ClientEntity Client { get; set; }
    public string? Identifier { get; set; }
    public string? Year { get; set; }
    public string? Model { get; set; }
    public string? Plate { get; set; }
    public IList<ServiceEntity>? Services { get; set; }
}


