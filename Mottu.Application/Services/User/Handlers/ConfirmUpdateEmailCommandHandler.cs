using MediatR;
using Desafio.Application.Common.Exceptions;
using Desafio.Application.Resources;
using Desafio.Application.Services.User.Commands.ConfirmUpdateEmail;
using Desafio.Domain.Entities;
using Desafio.Domain.Interfaces.Repository;

namespace Desafio.Application.Services.User.Handlers;
public class ConfirmUpdateEmailCommandHandler : IRequestHandler<ConfirmUpdateEmailCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IPreUpdateEmailRepository _preUpdateEmailRepository;

    public ConfirmUpdateEmailCommandHandler(IUserRepository userRepository, IPreUpdateEmailRepository preUpdateEmailRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _preUpdateEmailRepository = preUpdateEmailRepository ?? throw new ArgumentNullException(nameof(preUpdateEmailRepository));
    }

    public async Task<Unit> Handle(ConfirmUpdateEmailCommand request, CancellationToken cancellationToken)
    {
        #region Get and validate data
        var user = await _userRepository.SelectAsync(u => u.Id.Equals(request.User), cancellationToken: cancellationToken);
        if (user == null)
            throw new NotFoundException(nameof(UserEntity), request.User, GlobalMessages.NotFoundException);

        var preUpdateEmail = await _preUpdateEmailRepository.SelectAsync(p => p.Id.Equals(request.PreUpdate), cancellationToken: cancellationToken);
        if(preUpdateEmail == null)
            throw new NotFoundException(nameof(PreUpdateEmailEntity), request.PreUpdate, GlobalMessages.NotFoundException);

        bool isDataCorrect = user.UserMail == preUpdateEmail.OldEmail 
            && request.TokenValidation == preUpdateEmail.TokenValidation;

        if (!isDataCorrect)
            throw new ValidationException(nameof(PreUpdateEmailEntity), request.PreUpdate, GlobalMessages.NotFoundException);
        #endregion

        #region Update and delete Pre-Registration
        user.UserMail = preUpdateEmail.NewEmail;

        var userUpdated = await _userRepository.UpdateAsync(user, cancellationToken);

        if (userUpdated != null)
            await _preUpdateEmailRepository.DeleteAsync(p => p.Id.Equals(request.PreUpdate), cancellationToken);
        #endregion

        return Unit.Value;
    }
}
