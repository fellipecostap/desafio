using Desafio.Application.Common.Models;
using Desafio.Application.Services.User.Queries.GetUser;
using Desafio.Domain.Enums;
using MediatR;

namespace Desafio.Application.Services.User.Commands.CreateUser;
public class CreateUserCommand : IRequest<UserDto>
{
    public string? UserCNH { get; set; }

    public string? UserCNPJ { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public DateTime? BirthDate { get; set; }

    public string? UserMail { get; set; }

    public FileModel? CNHPhoto { get; set; }

    public CNHTypeEnum CNHType { get; set; }

    public Guid? UserType { get; set; }
}
