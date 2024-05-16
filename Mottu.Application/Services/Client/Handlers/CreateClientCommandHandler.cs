using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using Desafio.Application.Common.Exceptions;
using Desafio.Application.Common.Functions;
using Desafio.Application.Common.Models;
using Desafio.Application.Common.Storage;
using Desafio.Application.Resources;
using Desafio.Application.Services.Client.Commands.CreateClient;
using Desafio.Application.Services.Client.Queries.GetClient;
using Desafio.Domain.Entities;
using Desafio.Domain.Interfaces.Repository;

namespace Desafio.Application.Services.Client.Handlers;
public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, ClientDto>
{
    private readonly IClientRepository _clientRepository;
    private readonly IUserTypeRepository _userTypeRepository;
    private readonly IPreRegistrationRepository _preRegistrationRepository;
    private readonly IMapper _mapper;
    private readonly AWS _AWS;

    public CreateClientCommandHandler(IClientRepository clientRepository, IUserTypeRepository userTypeRepository, IPreRegistrationRepository preRegistrationRepository, IMapper mapper, IOptions<AWS> aws)
    {
        _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
        _userTypeRepository = userTypeRepository ?? throw new ArgumentNullException(nameof(userTypeRepository));
        _preRegistrationRepository = preRegistrationRepository ?? throw new ArgumentNullException(nameof(preRegistrationRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _AWS = aws.Value;
    }

    public async Task<ClientDto> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        #region Formating User

        var userType = await _userTypeRepository.SelectAsync(p => p.Id.Equals(request.ClientUser.UserType), null, true, cancellationToken);
        var selectNickNameByPreRegistration = await _preRegistrationRepository.SelectAsync(x => x.Email.Equals(request.ClientUser.UserMail), cancellationToken: cancellationToken);

        #region Get Password if necessary
        string pwd;
        if (string.IsNullOrEmpty(request.ClientUser.Password))
        {
            var getPwd = await _preRegistrationRepository.SelectAsync(p => p.Email.Equals(request.ClientUser.UserMail), null, true, cancellationToken);
            pwd = getPwd.Password;
        }
        else
        {
            pwd = request.ClientUser.Password;
        }
        #endregion

        var preRegistration = await _preRegistrationRepository.SelectAsync(x => x.Email.Equals(request.ClientUser.UserMail), null, true, cancellationToken);
        var userToCreate = new UserEntity
        {
            UserCnpj = request.ClientUser.UserCNPJ,
            UserName = request.ClientUser.UserName,
            Password = CommonFunctions.CreateMD5Hash(pwd),
            UserMail = request.ClientUser.UserMail,
            CNHType = request.ClientUser.CNHType,
            UserCNH = request.ClientUser.UserCNH,
            UserTypeEntity = userType,
        };

        #endregion

        #region Submit ProfilePhoto
        string photo;
        if (request.ProfilePhoto?.ContentFile != null)
        {
            if (!AmazonS3Service.IsValidImageFile(request.ProfilePhoto))
                throw new ValidationException(nameof(ClientEntity), request.ProfilePhoto, GlobalMessages.ProfilePhoto_MaximumLenght);

            var Getphoto = await AmazonS3Service.UploadObject(request.ProfilePhoto, _AWS);
            photo = AmazonS3Service.CreateLink(Getphoto.FileName, _AWS);
        }
        else
        {
            photo = null;
        }
        #endregion

        #region Validate if Exist
        var existCnh = await _clientRepository.ExistAsync(x => x.ClientUserEntity.UserCNH.Equals(request.ClientUser.UserCNH));
        var existCnpj = await _clientRepository.ExistAsync(x => x.ClientUserEntity.UserCnpj.Equals(request.ClientUser.UserCNPJ));
        if(existCnh.Equals(true) || existCnpj.Equals(true))
            throw new ValidationException(nameof(ClientEntity), request.ClientUser.CNHPhoto, GlobalMessages.CnpjOrCnhAlreadyExist);
        #endregion

        #region Submit CNHPhoto
        string cnhPhoto;
        if (request.ClientUser.CNHPhoto?.ContentFile != null)
        {
            if (!AmazonS3Service.IsValidImageFile(request.ClientUser.CNHPhoto))
                throw new ValidationException(nameof(ClientEntity), request.ClientUser.CNHPhoto, GlobalMessages.ProfilePhoto_MaximumLenght);

            var Getphoto = await AmazonS3Service.UploadObject(request.ClientUser.CNHPhoto, _AWS);
            cnhPhoto = AmazonS3Service.CreateLink(Getphoto.FileName, _AWS);
        }
        else
        {
            cnhPhoto = null;
        }
        userToCreate.CNHPhoto = cnhPhoto;
        #endregion

        #region Formating to create Client
        var clientToCreate = new ClientEntity
        {
            ClientUserEntity = userToCreate,
            ProfilePhoto = photo,
        };
        var clientCreated = await _clientRepository.InsertAsync(clientToCreate, cancellationToken);
        #endregion

        #region Clean PreRegistration

        await _preRegistrationRepository.DeleteAsync(l => l.Email.Equals(request.ClientUser.UserMail), cancellationToken);

        #endregion

        return _mapper.Map<ClientDto>(clientCreated);
    }
}
