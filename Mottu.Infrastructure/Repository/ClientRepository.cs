using Desafio.Domain.Entities;
using Desafio.Domain.Interfaces.Repository;
using Desafio.Infrastructure.Common;
using Desafio.Infrastructure.Context;

namespace Desafio.Infrastructure.Repository;
public class ClientRepository : BaseRepository<ClientEntity>, IClientRepository
{
    public ClientRepository(ApplicationDbContext context) : base(context)
    {

    }
}
