using Desafio.Application.Common.Mappings;
using Desafio.Domain.Entities;
using Desafio.Domain.Enums;

namespace Desafio.Application.Services.Plan.Dto;
public class PlanDto : IMapFrom<PlanEntity>
{
    public Guid Id { get; set; }

    public PlansEnum? Plan { get; set; }

    public double? Price { get; set; }
}
