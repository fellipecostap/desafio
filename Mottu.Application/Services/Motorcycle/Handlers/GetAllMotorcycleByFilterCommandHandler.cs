using AutoMapper;
using MediatR;
using Desafio.Application.Common.Functions;
using Desafio.Application.Services.Service.Commands.GetAllServicesByDateFilter;
using Desafio.Application.Services.Service.Queries.GetServiceById;
using Desafio.Domain.Entities;
using Desafio.Domain.Interfaces.Repository;
using System.Globalization;
using Desafio.Application.Services.Motorcycle.Commands.GetMotorcycleByFilter;
using Desafio.Application.Services.Client.Queries.GetClient;

namespace Desafio.Application.Services.Service.Handlers;
public class GetAllMotorcycleByFilterCommandHandler : IRequestHandler<GetAllMotorcycleByFilterCommand, MotorcycleVm>
{
    private readonly IMapper _mapper;
    private readonly IMotorcycleRepository _motorcycleRepository;

    public GetAllMotorcycleByFilterCommandHandler(IMapper mapper, IMotorcycleRepository motorcycleRepository)
    {
        _motorcycleRepository = motorcycleRepository ?? throw new ArgumentNullException(nameof(motorcycleRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<MotorcycleVm> Handle(GetAllMotorcycleByFilterCommand request, CancellationToken cancellationToken)
    {
        var selectMotorcycles = await _motorcycleRepository.SelectAllAsync(x => x.Plate.Equals(request.Plate));

        return new MotorcycleVm
        {
            MotorcycleList = _mapper.Map<List<MotorcycleDto>>(selectMotorcycles)
        };
    }
}
