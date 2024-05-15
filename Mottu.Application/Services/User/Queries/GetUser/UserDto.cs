using Desafio.Application.Common.Mappings;
using Desafio.Application.Services.UserType.Queries.GetUserType;
using Desafio.Domain.Entities;
using Desafio.Domain.Enums;

namespace Desafio.Application.Services.User.Queries.GetUser;
public class UserDto : IMapFrom<UserEntity>
{
    public Guid Id { get; set; }

    public CNHTypeEnum CNHType { get; set; }

    public string? UserCNH { get; set; } // Unico

    public string? CNHPhoto { get; set; }

    public string? UserCnpj { get; set; } // Unico

    public string? UserName { get; set; }

    public string? UserMail { get; set; }

    public DateTime? BirthDate { get; set; }

    public bool? UserExcluded { get; set; }

    public UserTypeDto? UserTypeEntity { get; set; }
}
