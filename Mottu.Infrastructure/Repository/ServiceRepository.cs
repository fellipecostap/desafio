using Desafio.Domain.Entities;
using Desafio.Domain.Interfaces.Repository;
using Desafio.Infrastructure.Common;
using Desafio.Infrastructure.Context;

namespace Desafio.Infrastructure.Repository;
public class ServiceRepository : BaseRepository<ServiceEntity>, IServiceRepository
{
    public ServiceRepository(ApplicationDbContext context) : base(context)
    {

    }
}
