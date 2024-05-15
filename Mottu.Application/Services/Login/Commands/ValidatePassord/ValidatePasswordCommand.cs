using System.Web.Mvc;
using MediatR;
using Desafio.Application.Services.Login.Queries.LoginUser;

namespace Desafio.Application.Services.Login.Commands.ValidatePassord;
public class ValidatePasswordCommand : IRequest<ValidatePasswordDto>
{
    public string? Email { get; set; }

    public string? Password { get; set; }

    public bool? InsertValidation { get; set; } = false;

}
