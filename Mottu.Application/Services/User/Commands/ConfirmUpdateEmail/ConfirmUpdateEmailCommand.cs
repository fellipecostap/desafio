using MediatR;

namespace Desafio.Application.Services.User.Commands.ConfirmUpdateEmail;
public class ConfirmUpdateEmailCommand : IRequest
{
    public string TokenValidation { get; set; }
    public Guid User { get; set; }
    public Guid PreUpdate { get; set; }
}
