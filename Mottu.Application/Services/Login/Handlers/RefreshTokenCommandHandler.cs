using AutoMapper;
using Desafio.Application.Common.Authorization;
using Desafio.Application.Common.Exceptions;
using Desafio.Application.Common.Models;
using Desafio.Application.Services.Client.Queries.GetClient;
using Desafio.Application.Services.Login.Commands.RefreshToken;
using Desafio.Application.Services.Login.Queries.LoginUser;
using Desafio.Domain.Entities;
using Desafio.Domain.Interfaces.Repository;
using MediatR;
using Microsoft.Extensions.Options;

namespace Desafio.Application.Services.Login.Handlers;
public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, LoginDto>
{

    private readonly Authentication _SecretKey;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;


    public RefreshTokenCommandHandler(IOptions<Authentication> options, IRefreshTokenRepository refreshTokenRepository, IClientRepository clientRepository, IUserRepository userRepository, IMapper mapper)
    {
        _SecretKey = options.Value;
        _refreshTokenRepository = refreshTokenRepository ?? throw new ArgumentNullException(nameof(refreshTokenRepository));
        _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<LoginDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        #region Refresh Token

        #region Get Claims to recieve token
        var principal = Auth.GetPrincipalFromExpiredToken(request.Token, _SecretKey.SecretKey);

        var UserId = new Guid(principal.Claims.ToList()[1].Value);

        List<string> listIncludeUser = new List<string>
            {
                $"{nameof(UserEntity.UserTypeEntity)}",
        };

        var user = _mapper.Map<UserEntity>(await _userRepository.SelectAsync(b => b.Id.Equals(UserId), listIncludeUser, false, cancellationToken: cancellationToken));

        var savedRefreshToken = await _refreshTokenRepository.SelectAsync(b => b.UserEntity.Id.Equals(UserId), cancellationToken: cancellationToken);

        if (savedRefreshToken.RefreshToken != request.RefreshToken)
            throw new ValidationException("Invalid refresh token", request.RefreshToken);
        #endregion

        #region Generate new Login Response

        var loginResponse = Auth.GenerateTokenFromRefresh(principal.Claims, _SecretKey.SecretKey);

        #endregion

        #region Generate new Refresh token and update register
        var newRefreshToken = Auth.GenerateRefreshToken();

        await _refreshTokenRepository.DeleteAsync(b => b.UserEntity.Id.Equals(UserId), cancellationToken);

        var RefreshTokenToCreate = new RefreshTokenEntity
        {
            UserEntity = user,
            RefreshToken = newRefreshToken
        };
        await _refreshTokenRepository.InsertAsync(RefreshTokenToCreate, cancellationToken);
        #endregion

        loginResponse.RefreshToken = newRefreshToken;

        #endregion

        #region Get DeliveryMan data

        List<string> listIncludeClient = new List<string>
            {
                $"{nameof(ClientEntity.ClientUserEntity)}.{nameof(UserEntity.UserTypeEntity)}",
            };
        var client = await _clientRepository.SelectAsync(b => b.ClientUserEntity.Id.Equals(user.Id), listIncludeClient, false, cancellationToken: cancellationToken);

        loginResponse.Client = _mapper.Map<ClientDto>(client);
        #endregion

        return loginResponse;
    }
}
