using MediatR;

namespace Desafio.Application.Services.PreRegistration.Commands.DeletePreRegistration;
public class DeletePreRegistrationCommand : IRequest
{
    public Guid Id { get; set; }
}


