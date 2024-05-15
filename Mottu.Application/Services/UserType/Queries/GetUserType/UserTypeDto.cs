using Desafio.Application.Common.Mappings;
using Desafio.Domain.Entities;

namespace Desafio.Application.Services.UserType.Queries.GetUserType;
public class UserTypeDto : IMapFrom<UserTypeEntity>
{
    public Guid Id { get; set; }
    public string? UserTypeName { get; set; }
}
