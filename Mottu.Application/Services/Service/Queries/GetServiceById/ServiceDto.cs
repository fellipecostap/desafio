using Desafio.Application.Common.Mappings;
using Desafio.Application.Services.Client.Queries.GetClient;
using Desafio.Application.Services.Plan.Dto;
using Desafio.Domain.Entities;
using Desafio.Domain.Enums;

namespace Desafio.Application.Services.Service.Queries.GetServiceById;
public class ServiceDto : IMapFrom<ServiceEntity>
{
    public Guid Id { get; set; }

    public ClientDto? Client { get; set; }

    public double? PricePerDay { get; set; }

    public double? Amount { get; set; }

    public double? Fine { get; set; }

    public PlanDto? Plan { get; set; }

    public DateTime? InitialDateTime { get; set; }

    public DateTime? FinalDateTime { get; set; }

    public DateTime? PrevisionFinalDateTime { get; set; }
}

