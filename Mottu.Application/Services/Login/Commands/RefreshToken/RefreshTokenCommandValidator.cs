using FluentValidation;

namespace Desafio.Application.Services.Login.Commands.RefreshToken;
public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {

    }
}
