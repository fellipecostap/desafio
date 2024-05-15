using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using Desafio.Application.Common.Exceptions;
using Desafio.Application.Common.Models;
using Desafio.Application.Resources;
using Desafio.Application.Services.Client.Commands.UpdateClient;
using Desafio.Application.Services.Client.Queries.GetClient;
using Desafio.Domain.Entities;
using Desafio.Domain.Interfaces.Repository;
using Desafio.Application.Services.Motorcycle.Commands.UpdateMotorcycle;

namespace Desafio.Application.Services.Client.Handlers;
public class UpdateMotorcycleCommandHandler : IRequestHandler<UpdateMotorcycleCommand, MotorcycleDto>
{
    private readonly IMotorcycleRepository _motorcycleRepository;
    private readonly IMapper _mapper;
    public UpdateMotorcycleCommandHandler(IMotorcycleRepository motorcycleRepository, IMapper mapper)
    {
        _motorcycleRepository = motorcycleRepository ?? throw new ArgumentNullException(nameof(motorcycleRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<MotorcycleDto> Handle(UpdateMotorcycleCommand request, CancellationToken cancellationToken)
    {
        List<string> listInclude = new List<string>
            {
                $"{nameof(MotorcycleEntity.Client)}"
            };

        var selectMotorcycle = await _motorcycleRepository.SelectAsync(x => x.Client.Id.Equals(request.ClientId), listInclude);

        selectMotorcycle.Plate = request.Plate;
        selectMotorcycle.Identifier = request.Identifier;
        selectMotorcycle.Year = request.Year;
        selectMotorcycle.Model = request.Model;

        var motorcycleUpdated = await _motorcycleRepository.UpdateAsync(selectMotorcycle);
        
        return _mapper.Map<MotorcycleDto>(motorcycleUpdated);
    }
}
