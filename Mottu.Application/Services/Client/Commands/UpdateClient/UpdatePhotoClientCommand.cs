using Desafio.Application.Common.Models;
using Desafio.Application.Services.Client.Queries.GetClient;
using MediatR;

namespace Desafio.Application.Services.Client.Commands.UpdateClient;
public class UpdatePhotoClientCommand : IRequest<ClientPhotoDto>
{
    public Guid ClientId { get; set; }
    public FileModel? ProfilePhoto { get; set; }
}
