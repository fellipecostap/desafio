using AutoMapper;
using MediatR;
using Desafio.Application.Common.Functions;
using Desafio.Application.Services.Service.Commands.GetAllServicesByDateFilter;
using Desafio.Application.Services.Service.Queries.GetServiceById;
using Desafio.Domain.Entities;
using Desafio.Domain.Interfaces.Repository;
using System.Globalization;

namespace Desafio.Application.Services.Service.Handlers;
public class GetAllServicesByDateFilterCommandHandler : IRequestHandler<GetAllServicesByDateFilterCommand, ServiceVm>
{
    private readonly IServiceRepository _serviceRepository;
    private readonly IMapper _mapper;
    public GetAllServicesByDateFilterCommandHandler(IServiceRepository serviceRepository, IMapper mapper )
    {
        _serviceRepository = serviceRepository ?? throw new ArgumentNullException(nameof(serviceRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<ServiceVm> Handle(GetAllServicesByDateFilterCommand request, CancellationToken cancellationToken)
    {
        List<string> listIncludeService = new List<string>
            {
                $"{nameof(ServiceEntity.ClientEntity)}",
            };

        var getAll = await _serviceRepository.SelectAllAsync(null, listIncludeService);

        return new ServiceVm
        {
            ServicesList = _mapper.Map<List<ServiceDto>>(getAll)
        };
    }
}
