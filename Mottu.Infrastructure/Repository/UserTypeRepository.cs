using Desafio.Domain.Entities;
using Desafio.Domain.Interfaces.Repository;
using Desafio.Infrastructure.Common;
using Desafio.Infrastructure.Context;

namespace Desafio.Infrastructure.Repository;
public class UserTypeRepository : BaseRepository<UserTypeEntity>, IUserTypeRepository
{
    public UserTypeRepository(ApplicationDbContext context) : base(context)
    {

    }
}
