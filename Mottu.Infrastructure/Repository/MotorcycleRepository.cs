using Desafio.Domain.Entities;
using Desafio.Domain.Interfaces.Repository;
using Desafio.Infrastructure.Common;
using Desafio.Infrastructure.Context;

namespace Desafio.Infrastructure.Repository;
public class MotorcycleRepository : BaseRepository<MotorcycleEntity>, IMotorcycleRepository
{
    public MotorcycleRepository(ApplicationDbContext context) : base(context)
    {

    }
}
