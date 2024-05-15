using AutoMapper;
using Desafio.Application.Common.Authorization;
using MediatR;
using Microsoft.Extensions.Options;
using Desafio.Application.Common.Exceptions;
using Desafio.Application.Common.Functions;
using Desafio.Application.Common.Models;
using Desafio.Application.Resources;
using Desafio.Application.Services.Client.Queries.GetClient;
using Desafio.Application.Services.Login.Commands.LoginUser;
using Desafio.Application.Services.Login.Queries.LoginUser;
using Desafio.Domain.Entities;
using Desafio.Domain.Interfaces.Repository;

namespace Desafio.Application.Services.Login.Handlers;
public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly Authentication _SecretKey;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IClientRepository _clientRepository;
    public LoginUserCommandHandler(IUserRepository userRepository, IMapper mapper, IOptions<Authentication> options, IRefreshTokenRepository refreshTokenRepository, IClientRepository clientRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _SecretKey = options.Value;
        _refreshTokenRepository = refreshTokenRepository ?? throw new ArgumentNullException(nameof(refreshTokenRepository));
        _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
    }

    public async Task<LoginDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        #region Login user

        #region Get User by request
        List<string> listIncludeUser = new List<string>
            {
                $"{nameof(UserEntity.UserTypeEntity)}",
            };
        var user = _mapper.Map<UserEntity>(await _userRepository.SelectAsync(b => b.UserMail.ToLower().Equals(request.Email.ToLower()) && b.Password.Equals(CommonFunctions.CreateMD5Hash(request.Password)), listIncludeUser, false, cancellationToken: cancellationToken));

        if (user == null)
            throw new ValidationException(GlobalMessages.Login_CannotAccess, request.Email);

        #endregion

        #region Generate token, refresh token and verify 
        var Authenticated = Auth.GenerateToken(user, _SecretKey.SecretKey);
        var sevenDaysBeforeNow = DateTime.Now.AddDays(-7);

        if (user.UserTypeEntity.Id.Equals(new Guid("6dfbf73d-0a5f-4e9c-822a-0d4290872b9d")))
        {
            var selectClient = await _clientRepository.SelectAsync(x => x.ClientUserEntity.Id.Equals(user.Id));
        }

        var RefreshToken = Auth.GenerateRefreshToken();

        var RefreshTokenToCreate = new RefreshTokenEntity
        {
            UserEntity = user,
            RefreshToken = RefreshToken
        };

        await _refreshTokenRepository.DeleteAsync(b => b.UserEntity.Id.Equals(user.Id), cancellationToken);
        await _refreshTokenRepository.InsertAsync(RefreshTokenToCreate, cancellationToken);

        Authenticated.RefreshToken = RefreshToken;
        #endregion


        #endregion

        #region Get Client data
        List<string> listIncludeClient = new List<string>
            {
                $"{nameof(ClientEntity.ClientUserEntity)}.{nameof(UserEntity.UserTypeEntity)}",
            };
        var client = await _clientRepository.SelectAsync(b => b.ClientUserEntity.Id.Equals(user.Id), listIncludeClient, false, cancellationToken: cancellationToken);

        Authenticated.Client = _mapper.Map<ClientDto>(client);
        #endregion

        return Authenticated;
    }
}
