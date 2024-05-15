using MediatR;
using Microsoft.Extensions.Options;
using Desafio.Application.Common.Exceptions;
using Desafio.Application.Common.Functions;
using Desafio.Application.Common.Models;
using Desafio.Application.Resources;
using Desafio.Application.Services.PreRegistration.Commands.ResendToken;
using Desafio.Application.Services.SendEmail.Commands.SendEmail.PreRegistration;
using Desafio.Domain.Entities;
using Desafio.Domain.Interfaces.Repository;
using System.Globalization;

namespace Desafio.Application.Services.PreRegistration.Handlers;
public class ResendTokenCommandHandler : IRequestHandler<ResendTokenCommand, Guid>
{
    private readonly IPreRegistrationRepository _preRegistrationRepository;
    private readonly EmailSettings _emailSettings;

    public ResendTokenCommandHandler(IPreRegistrationRepository preRegistrationRepository, IOptions<EmailSettings> options)
    {
        _preRegistrationRepository = preRegistrationRepository ?? throw new ArgumentNullException(nameof(preRegistrationRepository));
        _emailSettings = options.Value;
    }

    public async Task<Guid> Handle(ResendTokenCommand request, CancellationToken cancellationToken)
    {
        #region Get all data of PreRegistration
        var entity = await _preRegistrationRepository.SelectAsync(l => l.Id.Equals(request.Id), null, true, cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(PreRegistrationEntity), request.Id, GlobalMessages.NotFoundException);
        #endregion

        #region Check the time of another record with the same data

        var verifyRegister = await _preRegistrationRepository.SelectAllAsync(l => l.Email.Equals(entity.Email), null, true, cancellationToken);

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
                throw new ValidationException(
                    GlobalMessages.PreRegistration_Token_LimitTime, LastRegister.Email);
            }
        }

        #endregion

        #region Generate a new token

        var token = "";
        var confirmToken = true;

        while (confirmToken == true)
        {
            token = CommonFunctions.GenerateConfirmToken();

            confirmToken = await _preRegistrationRepository.ExistAsync(l => l.TokenValidation.Equals(token) && l.Email.Equals(entity.Email), cancellationToken);
        }

        #endregion

        #region Insert a data to PreRegistration 

        PreRegistrationEntity entityResponse;

        if (verifyRegister.Count() >= 1)
        {
            var LastRegister = verifyRegister.OrderBy(c => c.Created).Last();

            LastRegister.TokenValidation = token;

            entityResponse = await _preRegistrationRepository.UpdateAsync(LastRegister, cancellationToken);

        }
        else
        {
            var EntityToCreate = new PreRegistrationEntity
            {
                Email = entity.Email,
                Password = entity.Password,
                TagValidation = false,
                TokenValidation = token,
            };

            entityResponse = await _preRegistrationRepository.InsertAsync(EntityToCreate);
        }
        #endregion

        #region Send email with new token

        var EmailData = new SendEmailPreRegistrationCommand { UserEmail = entity.Email, UserToken = token };

        EmailFunctions.SendEmailPreRegistration(EmailData, _emailSettings);

        #endregion

        return entityResponse.Id;

    }
}
