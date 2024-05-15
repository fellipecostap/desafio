using MediatR;
using Microsoft.Extensions.Options;
using Desafio.Application.Common.Exceptions;
using Desafio.Application.Common.Functions;
using Desafio.Application.Common.Models;
using Desafio.Application.Resources;
using Desafio.Application.Services.PreRegistration.Commands.CreatePreRegistration;
using Desafio.Application.Services.SendEmail.Commands.SendEmail.PreRegistration;
using Desafio.Domain.Entities;
using Desafio.Domain.Interfaces.Repository;
using System.Globalization;

namespace Desafio.Application.Services.PreRegistration.Handlers;
public class CreatePreRegistrationCommandHandler : IRequestHandler<CreatePreRegistrationCommand, Guid>
{
    private readonly IPreRegistrationRepository _preRegistrationRepository;
    private readonly EmailSettings _emailSettings;
    private readonly IUserRepository _userRepository;

    public CreatePreRegistrationCommandHandler(IPreRegistrationRepository preRegistrationRepository, IOptions<EmailSettings> options, IUserRepository userRepository)
    {

        _preRegistrationRepository = preRegistrationRepository ?? throw new ArgumentNullException(nameof(preRegistrationRepository));
        _emailSettings = options.Value;
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<Guid> Handle(CreatePreRegistrationCommand request, CancellationToken cancellationToken)
    {
        #region Check if the email exist in users
        var verifyUser = await _userRepository.ExistAsync(l => l.UserMail.Equals(request.Email), cancellationToken);

        if (verifyUser)
            throw new NotFoundException(GlobalMessages.PreRegistration_EmailExist);
        #endregion

        #region Check the time of another record with the same data

        var verifyRegister = await _preRegistrationRepository.SelectAllAsync(l => l.Email.Equals(request.Email), null, true, cancellationToken);

        if (verifyRegister.Count() >= 1)
        {

            var LastRegister = verifyRegister.OrderBy(c => c.Created).Last();
            string DateToSearch;
            if (LastRegister.LastModified != null)
                DateToSearch = LastRegister.LastModified;
            else
                DateToSearch = LastRegister.Created;

            var culture = new CultureInfo("en-US");
            DateTime RegisterDate = Convert.ToDateTime(DateToSearch, culture);
            var diffDates = DateTime.Now - RegisterDate;

            if (diffDates.TotalMinutes < 1)
            {
                throw new ValidationException(GlobalMessages.PreRegistration_Token_LimitTime, LastRegister.Email);
            }
        }

        #endregion

        #region Generate token

        var token = "";
        var confirmToken = true;

        while (confirmToken == true)
        {
            //Generating a new token
            token = CommonFunctions.GenerateConfirmToken();

            //Checking generated token and email sent in database
            confirmToken = await _preRegistrationRepository.ExistAsync(l => l.TokenValidation.Equals(token) && l.Email.Equals(request.Email), cancellationToken);

        }

        #endregion

        #region Insert a new PreRegistration
        PreRegistrationEntity entityResponse;

        if (verifyRegister.Count() >= 1)
        {
            var LastRegister = verifyRegister.OrderBy(c => c.Created).Last();

            LastRegister.TokenValidation = token;

            entityResponse = await _preRegistrationRepository.UpdateAsync(LastRegister, cancellationToken);

        }
        else
        {
            //Formats entity for creation
            var EntityToCreate = new PreRegistrationEntity
            {
                Email = request.Email,
                Password = CommonFunctions.CreateMD5Hash(request.Password),
                TagValidation = false,
                TokenValidation = token,
                NickName = request.NickName
            };

            //Performs insert
            entityResponse = await _preRegistrationRepository.InsertAsync(EntityToCreate, cancellationToken);
        }


        #endregion

        #region Send email with token

        var EmailData = new SendEmailPreRegistrationCommand { UserEmail = request.Email, UserToken = token, NickName = request.NickName };
        if (string.IsNullOrEmpty(EmailData.UserEmail))
        {
            throw new NotFoundException(GlobalMessages.PreRegistration_EmailExist);
        }
        else
        {
            EmailFunctions.SendEmailPreRegistration(EmailData, _emailSettings);
        }

        #endregion

        return entityResponse.Id;
    }
}
