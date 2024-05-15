using MediatR;
using Microsoft.Extensions.Options;
using Desafio.Application.Common.Exceptions;
using Desafio.Application.Common.Functions;
using Desafio.Application.Common.Models;
using Desafio.Application.Resources;
using Desafio.Application.Services.Login.Commands.ForgotPassword;
using Desafio.Application.Services.SendEmail.Commands.SendEmail.PreRegistration;
using Desafio.Domain.Entities;
using Desafio.Domain.Interfaces.Repository;

namespace Desafio.Application.Services.Login.Handlers;
public class ResendTokenCommandHandler : IRequestHandler<ForgotPasswordCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserTypeRepository _userTypeRepository;
    private readonly IPreRegistrationRepository _preRegistrationRepository;
    private readonly EmailSettings _emailSettings;

    public ResendTokenCommandHandler(IUserRepository userRepository, IUserTypeRepository userTypeRepository, IPreRegistrationRepository preRegistrationRepository, IOptions<EmailSettings> options)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _userTypeRepository = userTypeRepository ?? throw new ArgumentNullException(nameof(userTypeRepository));
        _preRegistrationRepository = preRegistrationRepository ?? throw new ArgumentNullException(nameof(preRegistrationRepository));
        _emailSettings = options.Value;
    }

    public async Task<Guid> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        #region Get all data of User


        var entity = new UserEntity();
        if (request.UserType != null)
        {

            var GetUsertype = await _userTypeRepository.SelectAsync(l => l.Id.Equals(request.UserType), null, true, cancellationToken);
            if (GetUsertype == null)
                throw new NotFoundException(nameof(UserTypeEntity), request.UserType, GlobalMessages.NotFoundException);

            entity = await _userRepository.SelectAsync(l => l.UserTypeEntity.Equals(GetUsertype) && l.UserCnpj.Equals(request.Parameter), null, true, cancellationToken);
        }
        else
        {
            entity = await _userRepository.SelectAsync(l => l.UserMail.Equals(request.Parameter), null, true, cancellationToken);
        }

        if (entity == null)
            throw new NotFoundException(nameof(PreRegistrationEntity), request.Parameter, GlobalMessages.NotFoundException);

        #endregion

        #region Generate a token

        //Generating a new token
        var token = CommonFunctions.GenerateConfirmToken();

        #endregion

        #region Confirm if email it is not in Pregistation table

        if (await _preRegistrationRepository.ExistAsync(l => l.Email.Equals(entity.UserMail), cancellationToken))
        {
            await _preRegistrationRepository.DeleteAsync(l => l.Email.Equals(entity.UserMail), cancellationToken);
        }

        #endregion

        #region Insert a data to PreRegistration 

        //Formats entity for creation
        var EntityToCreate = new PreRegistrationEntity
        {
            Email = entity.UserMail,
            Password = "",
            TagValidation = false,
            TokenValidation = token,
        };

        //Performs insert
        var entityResponse = await _preRegistrationRepository.InsertAsync(EntityToCreate, cancellationToken);

        #endregion

        #region Send email With new token
        var EmailData = new SendEmailPreRegistrationCommand { UserEmail = entity.UserMail, UserToken = token };

        EmailFunctions.SendEmailPreRegistration(EmailData, _emailSettings);
        #endregion

        return entityResponse.Id;

    }
}
