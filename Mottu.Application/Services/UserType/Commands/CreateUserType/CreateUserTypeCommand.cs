using MediatR;
using Desafio.Application.Services.UserType.Queries.GetUserType;

namespace Desafio.Application.Services.UserType.Commands.CreateUserType;
public class CreateUserTypeCommand : IRequest<UserTypeDto>
{
    public string? UserTypeName { get; set; }
}
