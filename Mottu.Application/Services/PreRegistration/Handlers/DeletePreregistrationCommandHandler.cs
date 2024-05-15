using MediatR;
using Desafio.Application.Common.Exceptions;
using Desafio.Application.Resources;
using Desafio.Application.Services.PreRegistration.Commands.DeletePreRegistration;
using Desafio.Domain.Entities;
using Desafio.Domain.Interfaces.Repository;
namespace Desafio.Application.Services.PreRegistration.Handlers;
public class DeletePreregistrationCommandHandler : IRequestHandler<DeletePreRegistrationCommand>
{
    private readonly IPreRegistrationRepository _preRegistrationRepository;

    public DeletePreregistrationCommandHandler(IPreRegistrationRepository preRegistrationRepository) =>
        _preRegistrationRepository = preRegistrationRepository ?? throw new ArgumentNullException(nameof(preRegistrationRepository));

    public async Task<Unit> Handle(DeletePreRegistrationCommand request, CancellationToken cancellationToken)
    {
        return await _preRegistrationRepository.DeleteAsync(l => l.Id.Equals(request.Id), cancellationToken)
            ? Unit.Value
            : throw new NotFoundException(nameof(PreRegistrationEntity), request.Id, GlobalMessages.NotFoundException);
    }
}
