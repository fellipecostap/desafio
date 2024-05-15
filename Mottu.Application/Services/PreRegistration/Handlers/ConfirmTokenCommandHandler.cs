using MediatR;
using Desafio.Application.Common.Exceptions;
using Desafio.Application.Resources;
using Desafio.Application.Services.PreRegistration.Commands.ConfirmToken;
using Desafio.Domain.Interfaces.Repository;

namespace Desafio.Application.Services.PreRegistration.Handlers;
public class ConfirmTokenCommandHandler : IRequestHandler<ConfirmTokenCommand>
{
    private readonly IPreRegistrationRepository _preRegistrationRepository;


    public ConfirmTokenCommandHandler(IPreRegistrationRepository preRegistrationRepository)
    {
        _preRegistrationRepository = preRegistrationRepository ?? throw new ArgumentNullException(nameof(preRegistrationRepository));
    }

    public async Task<Unit> Handle(ConfirmTokenCommand request, CancellationToken cancellationToken)
    {
        #region Persists in the database according to the parameters

        var entity = await _preRegistrationRepository.SelectAsync(l => l.TokenValidation.Equals(request.Token) && l.Id.Equals(request.Id), null, true, cancellationToken);

        if (entity == null)
            throw new NotFoundException(GlobalMessages.PreRegistration_ConfirmToken_Invalid);
        #endregion

        #region Update the record with token validation

        entity.TagValidation = true;

        await _preRegistrationRepository.UpdateAsync(entity, cancellationToken);

        #endregion

        return Unit.Value;
    }
}
