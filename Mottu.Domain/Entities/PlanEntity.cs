using Desafio.Domain.Common;
using Desafio.Domain.Enums;

namespace Desafio.Domain.Entities;
public class PlanEntity : AuditableEntity
{
    public PlansEnum? Plan { get; set; }

    public double Price { get; set; }

    public double Fine { get; set; }
}
