using Desafio.Application.Common.Exceptions;
using Desafio.Application.Resources;
using Desafio.Application.Services.Motorcycle.Commands.DeleteMotorcycle;
using Desafio.Domain.Entities;
using Desafio.Domain.Interfaces.Repository;
using MediatR;

namespace Desafio.Application.Services.Client.Handlers;
public class DeleteMotorcycleCommandHandler : IRequestHandler<DeleteMotorcycleCommand, Unit>
{
    private readonly IMotorcycleRepository _motorcycleRepository;
    public DeleteMotorcycleCommandHandler(IMotorcycleRepository motorcycleRepository)
    {
        _motorcycleRepository = motorcycleRepository ?? throw new ArgumentNullException(nameof(motorcycleRepository));
    }

    public async Task<Unit> Handle(DeleteMotorcycleCommand request, CancellationToken cancellationToken)
    {
        return await _motorcycleRepository.DeleteAsync(c => c.Id.Equals(request.MotorcycleId), cancellationToken: cancellationToken)
            ? Unit.Value
            : throw new NotFoundException(nameof(MotorcycleEntity), request.MotorcycleId, GlobalMessages.NotFoundException);
    }
}
