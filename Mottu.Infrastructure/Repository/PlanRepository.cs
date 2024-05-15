using Desafio.Domain.Entities;
using Desafio.Domain.Interfaces.Repository;
using Desafio.Infrastructure.Common;
using Desafio.Infrastructure.Context;

namespace Desafio.Infrastructure.Repository;
public class PlanRepository : BaseRepository<PlanEntity>, IPlanRepository
{
    public PlanRepository(ApplicationDbContext context) : base(context)
    {

    }
}
