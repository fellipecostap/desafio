using AutoMapper;
using MediatR;
using Desafio.Application.Common.Exceptions;
using Desafio.Application.Resources;
using Desafio.Application.Services.PreRegistration.Commands.GetPreRegistrationById;
using Desafio.Application.Services.PreRegistration.Queries.GetPreRegistrations;
using Desafio.Domain.Entities;
using Desafio.Domain.Interfaces.Repository;

namespace Desafio.Application.Services.PreRegistration.Handlers;
public class GetPreRegistrationByIdCommandHandler : IRequestHandler<GetPreRegistrationByIdCommand, PreRegistrationDto>
{
    private readonly IPreRegistrationRepository _preRegistrationRepository;
    private readonly IMapper _mapper;

    public GetPreRegistrationByIdCommandHandler(IPreRegistrationRepository preRegistrationRepository, IMapper mapper)
    {
        _preRegistrationRepository = preRegistrationRepository ?? throw new ArgumentNullException(nameof(preRegistrationRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<PreRegistrationDto> Handle(GetPreRegistrationByIdCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<PreRegistrationDto>(await _preRegistrationRepository.SelectAsync(l => l.Id.Equals(request.Id), null, true, cancellationToken));

        if (entity == null)
            throw new NotFoundException(nameof(PreRegistrationEntity), request.Id, GlobalMessages.NotFoundException);

        return entity;
    }
}
