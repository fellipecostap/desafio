using Desafio.Domain.Entities;
using Desafio.Domain.Interfaces.Repository;
using Desafio.Infrastructure.Common;
using Desafio.Infrastructure.Context;

namespace Desafio.Infrastructure.Repository;
public class PreRegistrationRepository : BaseRepository<PreRegistrationEntity>, IPreRegistrationRepository
{
    public PreRegistrationRepository(ApplicationDbContext context) : base(context)
    {

    }
}
