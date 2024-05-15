using Desafio.Application.Common.Mappings;
using Desafio.Domain.Entities;

namespace Desafio.Application.Services.PreRegistration.Queries.GetPreRegistrations;
public class PreRegistrationDto : IMapFrom<PreRegistrationEntity>
{
    public Guid Id { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? TokenValidation { get; set; }

    public bool TagValidation { get; set; }
}
