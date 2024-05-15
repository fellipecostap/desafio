using Desafio.Application.Services.UserType.Queries.GetUserType;

namespace Desafio.Application.Services.User.Queries.GetUser;
public class UserVm
{
    public IList<UserTypeDto> UserTypes { get; set; } = new List<UserTypeDto>();
    public IList<UserDto> List { get; set; } = new List<UserDto>();

}
