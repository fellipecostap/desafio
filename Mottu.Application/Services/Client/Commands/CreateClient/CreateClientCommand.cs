using MediatR;
using Desafio.Application.Common.Models;
using Desafio.Application.Services.Client.Queries.GetClient;
using Desafio.Application.Services.User.Commands.CreateUser;

namespace Desafio.Application.Services.Client.Commands.CreateClient;
public class CreateClientCommand : IRequest<ClientDto>
{
    public CreateUserCommand? ClientUser { get; set; }

    public FileModel? ProfilePhoto { get; set; }
}
