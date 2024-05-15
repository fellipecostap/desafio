using MediatR;
using Desafio.Application.Services.PreRegistration.Queries.GetPreRegistrations;

namespace Desafio.Application.Services.PreRegistration.Commands.GetPreRegistrationById;
public class GetPreRegistrationByIdCommand : IRequest<PreRegistrationDto>
{
    public Guid Id { get; set; }
}
