using MediatR;
using Desafio.Application.Common.Exceptions;
using Desafio.Application.Common.Functions;
using Desafio.Application.Resources;
using Desafio.Application.Services.Login.Commands.ChangePassword;
using Desafio.Domain.Entities;
using Desafio.Domain.Interfaces.Repository;

namespace Desafio.Application.Services.Login.Handlers;
public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IPreRegistrationRepository _preRegistrationRepository;


    public ChangePasswordCommandHandler(IUserRepository userRepository, IPreRegistrationRepository preRegistrationRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _preRegistrationRepository = preRegistrationRepository ?? throw new ArgumentNullException(nameof(preRegistrationRepository));
    }

    public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        #region Verify token validation
        var PreRegistration = await _preRegistrationRepository.SelectAsync(l => l.Id.Equals(request.Id), null, true, cancellationToken);

        if (PreRegistration == null)
            throw new NotFoundException(nameof(PreRegistrationEntity), request.Id, GlobalMessages.NotFoundException);
        else if (!PreRegistration.TagValidation)
            throw new NotFoundException(nameof(PreRegistrationEntity), request.Id, GlobalMessages.NotFoundException);
        #endregion

        #region Update a data to User
        var user = await _userRepository.SelectAsync(l => l.UserMail.Equals(PreRegistration.Email), null, true, cancellationToken);
        if (user == null)
            throw new NotFoundException(nameof(UserEntity), request.Id, GlobalMessages.NotFoundException);

        user.Password = CommonFunctions.CreateMD5Hash(request.NewPassword);

        //Performs insert
        var userResponse = await _userRepository.UpdateAsync(user);
        if (userResponse != null)
        {
            await _preRegistrationRepository.DeleteAsync(l => l.Id.Equals(request.Id), cancellationToken);
        }
        #endregion

        return Unit.Value;
    }
}
