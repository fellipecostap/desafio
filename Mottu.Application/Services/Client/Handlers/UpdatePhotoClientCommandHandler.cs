using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using Desafio.Application.Common.Exceptions;
using Desafio.Application.Common.Models;
using Desafio.Application.Common.Storage;
using Desafio.Application.Resources;
using Desafio.Application.Services.Client.Commands.UpdateClient;
using Desafio.Application.Services.Client.Queries.GetClient;
using Desafio.Domain.Entities;
using Desafio.Domain.Interfaces.Repository;

namespace Desafio.Application.Services.Client.Handlers;
public class UpdatePhotoClientCommandHandler : IRequestHandler<UpdatePhotoClientCommand, ClientPhotoDto>
{
    private readonly IClientRepository _clientRepository;
    private readonly IMapper _mapper;
    private readonly AWS _AWS;

    public UpdatePhotoClientCommandHandler(IClientRepository clientRepository, IMapper mapper, IOptions<AWS> aws)
    {
        _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _AWS = aws.Value;
    }

    public async Task<ClientPhotoDto> Handle(UpdatePhotoClientCommand request, CancellationToken cancellationToken)
    {
        #region Search for Client
        var client = await _clientRepository.SelectAsync(v => v.Id.Equals(request.ClientId), cancellationToken: cancellationToken);
        if (client == null)
            throw new NotFoundException(nameof(ClientEntity), request.ClientId, GlobalMessages.NotFoundException);
            #endregion

        if (request.ProfilePhoto?.HasChange == true)
        {
            #region Submit ProfilePhoto
            string? photo;
            if (request.ProfilePhoto.ContentFile != null)
            {
                if (!AmazonS3Service.IsValidImageFile(request.ProfilePhoto))
                    throw new ValidationException(nameof(ClientEntity), request.ProfilePhoto, GlobalMessages.ProfilePhoto_MaximumLenght);

                var Getphoto = await AmazonS3Service.UploadObject(request.ProfilePhoto, _AWS);
                photo = AmazonS3Service.CreateLink(Getphoto.FileName, _AWS);

                if (client.ProfilePhoto != null)
                    await AmazonS3Service.RemoveObject(AmazonS3Service.GetObjectByLink(client.ProfilePhoto), _AWS);
            }
            else
            {
                photo = null;
            }
            #endregion

            client.ProfilePhoto = photo;

            var clientUpdated = await _clientRepository.UpdateAsync(client, cancellationToken: cancellationToken);
            return _mapper.Map<ClientPhotoDto>(clientUpdated);
        }
        else
            return new ClientPhotoDto() { ProfilePhoto = client.ProfilePhoto };
    }
}
