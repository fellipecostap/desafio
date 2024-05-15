using Desafio.Domain.Entities;
using Desafio.Domain.Interfaces.Repository;
using Desafio.Infrastructure.Common;
using Desafio.Infrastructure.Context;

namespace Desafio.Infrastructure.Repository;
public class RefreshTokenRepository : BaseRepository<RefreshTokenEntity>, IRefreshTokenRepository
{
    public RefreshTokenRepository(ApplicationDbContext context) : base(context)
    {

    }
}
