using System.Diagnostics.CodeAnalysis;

namespace Desafio.Domain.Security;

[ExcludeFromCodeCoverage]
public class TokenConfigurations
{
    public string Audience { get; set; }
    public string Issuer { get; set; }
    public int Seconds { get; set; }
}

