using AutoMapper;
using MediatR;
using Desafio.Application.Services.PreRegistration.Queries.GetPreRegistrations;
using Desafio.Domain.Interfaces.Repository;

namespace Desafio.Application.Services.PreRegistration.Handlers;
public class GetPreRegistrationsQueriesHandler : IRequestHandler<GetPreRegistrationsQuery, PreRegistrationVm>
{
    private readonly IPreRegistrationRepository _preRegistrationRepository;
    private readonly IMapper _mapper;

    public GetPreRegistrationsQueriesHandler(IPreRegistrationRepository preRegistrationRepository, IMapper mapper)
    {
        _preRegistrationRepository = preRegistrationRepository ?? throw new ArgumentNullException(nameof(preRegistrationRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<PreRegistrationVm> Handle(GetPreRegistrationsQuery request, CancellationToken cancellationToken)
    {
        return new PreRegistrationVm
        {
            List = _mapper.Map<IList<PreRegistrationDto>>(await _preRegistrationRepository.SelectAllAsync(null, null, true, cancellationToken: cancellationToken))
        };
    }
}
