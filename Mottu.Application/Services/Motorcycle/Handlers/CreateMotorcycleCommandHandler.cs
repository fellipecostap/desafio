using AutoMapper;
using Desafio.Application.Services.Client.Queries.GetClient;
using Desafio.Application.Services.Motorcycle.Commands.CreateMotorcycle;
using Desafio.Application.Services.Service.Queries.GetServiceById;
using Desafio.Domain.Entities;
using Desafio.Domain.Interfaces.Repository;
using MediatR;

namespace Desafio.Application.Services.Service.Handlers;
public class CreateMotorcycleCommandHandler : IRequestHandler<CreateMotorcycleCommand, MotorcycleDto>
{
    private readonly IClientRepository _clientRepository;
    private readonly IMapper _mapper;
    private readonly IMotorcycleRepository _motorcycleRepository;
    public CreateMotorcycleCommandHandler(IClientRepository clientRepository, IMapper mapper, IMotorcycleRepository motorcycleRepository)
    {
        _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _motorcycleRepository = motorcycleRepository ?? throw new ArgumentNullException(nameof(motorcycleRepository));
    }

    public async Task<MotorcycleDto> Handle(CreateMotorcycleCommand request, CancellationToken cancellationToken)
    {
        var selectClient = await _clientRepository.SelectAsync(x => x.Id.Equals(request.ClientId));

        var newMoto = new MotorcycleEntity()
        {
            Client = selectClient,
            Identifier = request.Identifier,
            Model = request.Model,
            Plate = request.Plate,
            Year = request.Year
        };

        var insertMoto = await _motorcycleRepository.InsertAsync(newMoto);

        return _mapper.Map<MotorcycleDto>(insertMoto);
    }
}
