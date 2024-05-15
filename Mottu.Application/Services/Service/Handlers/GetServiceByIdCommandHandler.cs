using AutoMapper;
using MediatR;
using Desafio.Application.Common.Exceptions;
using Desafio.Application.Resources;
using Desafio.Application.Services.Service.Commands.GetServiceById;
using Desafio.Application.Services.Service.Queries.GetServiceById;
using Desafio.Domain.Entities;
using Desafio.Domain.Interfaces.Repository;

namespace Desafio.Application.Services.Service.Handlers;
public class GetServiceByIdCommandHandler : IRequestHandler<GetServiceByIdCommand, ServiceDto>
{
    private readonly IServiceRepository _serviceRepository;
    private readonly IMapper _mapper;
    public GetServiceByIdCommandHandler(IServiceRepository serviceRepository, IMapper mapper)
    {
        _serviceRepository = serviceRepository ?? throw new ArgumentNullException(nameof(serviceRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<ServiceDto> Handle(GetServiceByIdCommand request, CancellationToken cancellationToken)
    {
        List<string> listIncludeService = new List<string>
            {
                $"{nameof(ServiceEntity.ClientEntity)}",
            };

        var serviceSelected = await _serviceRepository.SelectAsync(c => c.Id.Equals(request.ServiceId), listIncludeService, cancellationToken: cancellationToken);

        if (serviceSelected == null)
            throw new NotFoundException(nameof(ServiceEntity), request.ServiceId, GlobalMessages.NotFoundException);

        return _mapper.Map<ServiceDto>(serviceSelected);
    }
}
