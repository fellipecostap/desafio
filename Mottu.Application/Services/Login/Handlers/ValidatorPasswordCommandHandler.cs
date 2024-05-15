using AutoMapper;
using MediatR;
using Desafio.Application.Common.Exceptions;
using Desafio.Application.Common.Functions;
using Desafio.Application.Resources;
using Desafio.Application.Services.Login.Commands.ValidatePassord;
using Desafio.Application.Services.Login.Queries.LoginUser;
using Desafio.Domain.Entities;
using Desafio.Domain.Interfaces.Repository;

namespace Desafio.Application.Services.Login.Handlers;
public class ValidatorPasswordCommandHandler : IRequestHandler<ValidatePasswordCommand, ValidatePasswordDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPreRegistrationRepository _preRegistrationRepository;


    public ValidatorPasswordCommandHandler(IUserRepository userRepository, IMapper mapper, IPreRegistrationRepository preRegistrationRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _preRegistrationRepository = preRegistrationRepository ?? throw new ArgumentNullException(nameof(preRegistrationRepository));
    }

    public async Task<ValidatePasswordDto> Handle(ValidatePasswordCommand request, CancellationToken cancellationToken)
    {
        #region Get User by request and validate with token
        var user = _mapper.Map<UserEntity>(await _userRepository.SelectAsync(b => b.UserMail.ToLower().Equals(request.Email.ToLower()) && b.Password.Equals(CommonFunctions.CreateMD5Hash(request.Password)), cancellationToken: cancellationToken));

        if (user == null)
            throw new ValidationException(GlobalMessages.Login_WrongPassword, request.Email);
        Guid IdToReturn = Guid.Empty;
        #endregion

        #region Confirm if email it is not in PreRegistration table
        var verifyExistPreRegistration = await _preRegistrationRepository.ExistAsync(l => l.Email.Equals(user.UserMail), cancellationToken);
        if (verifyExistPreRegistration)
        {
            await _preRegistrationRepository.DeleteAsync(l => l.Email.Equals(user.UserMail), cancellationToken);
        }
        #endregion

        #region Insert a validated data to PreRegistration to change password in next step of app
        if (request.InsertValidation != null && request.InsertValidation == true)
        {
            var EntityToCreate = new PreRegistrationEntity
            {
                Email = user.UserMail,
                Password = "",
                TagValidation = true,
                TokenValidation = "",
            };

            var preRegistrationInserted = await _preRegistrationRepository.InsertAsync(EntityToCreate, cancellationToken);
            IdToReturn = preRegistrationInserted.Id;
        }
        #endregion

        return new ValidatePasswordDto() { Id = IdToReturn };
    }
}
