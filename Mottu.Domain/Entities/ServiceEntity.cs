using Desafio.Domain.Common;
using Desafio.Domain.Enums;

namespace Desafio.Domain.Entities;
public class ServiceEntity : AuditableEntity
{
    public ClientEntity? ClientEntity { get; set; }

    public PlanEntity? Plan { get; set; }

    public double? Amount { get; set; } // Valor total

    public double? Price { get; set; }

    public double? Fine { get; set; } // Multa devida

    public string? InitialDateTime { get; set; }

    public string? FinalDateTime { get; set; }

    public string? PrevisionFinalDateTime { get; set; }
}
