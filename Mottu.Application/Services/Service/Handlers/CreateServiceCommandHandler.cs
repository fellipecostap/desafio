using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using Desafio.Application.Common.Functions;
using Desafio.Application.Common.Models;
using Desafio.Application.Services.Service.Commands.CreateService;
using Desafio.Application.Services.Service.Queries.GetServiceById;
using Desafio.Domain.Interfaces.Repository;
using Desafio.Domain.Entities;
using Desafio.Domain.Enums;
using Desafio.Application.Resources;
using Desafio.Application.Common.Exceptions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Desafio.Application.Services.Service.Handlers;
public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, ServiceDto>
{
    private readonly IServiceRepository _serviceRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IMapper _mapper;
    private readonly IPlanRepository _planRepository;
    private readonly EmailSettings _emailSettings;
    private readonly CommonFunctions _commonFunctions;

    public CreateServiceCommandHandler(IServiceRepository serviceRepository, IClientRepository clientRepository, IMapper mapper, IOptions<EmailSettings> options, CommonFunctions commonFunctions, IPlanRepository planRepository)
    {
        _serviceRepository = serviceRepository ?? throw new ArgumentNullException(nameof(serviceRepository));
        _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _emailSettings = options.Value;
        _commonFunctions = commonFunctions;
        _planRepository = planRepository ?? throw new ArgumentNullException(nameof(planRepository));
    }

    public async Task<ServiceDto> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
    {
        List<string> listIncludeClient = new List<string>
            {
                $"{nameof(ClientEntity.ClientUserEntity)}",
            };

        var selectClient = await _clientRepository.SelectAsync(x => x.Id.Equals(request.ClientId), listIncludeClient);

        #region Calculate Price
        var selectPlan = await _planRepository.SelectAsync(x => x.Plan.Equals(request.Plan));

        PlansEnum plan = selectPlan.Plan.Value;
        int days = (int)plan;

        var amount = days * selectPlan.Price;
        #endregion

        if (!selectClient.ClientUserEntity.CNHType.Equals(CNHTypeEnum.A) || selectClient.ClientUserEntity.CNHType.Equals(CNHTypeEnum.AB))
            throw new ValidationException(nameof(ClientEntity), request.ClientId, GlobalMessages.CnhInvalid);

        if (!request.InitialDateTime.Equals(DateTime.Today.AddDays(1)))
            throw new ValidationException(nameof(ClientEntity), request.ClientId, GlobalMessages.InitialDateTimeInvalid);

        double fine = 0;

        if (request.FinalDateTime < request.PrevisionFinalDateTime)
        {
            TimeSpan? diference = request.PrevisionFinalDateTime.Value - request.FinalDateTime.Value;
            int daysDiference = diference.Value.Days;

            var pricePerDayWithFine = selectPlan.Price * selectPlan.Fine;

            fine = selectPlan.Fine;

            var amountWithDiferenceDays = daysDiference * pricePerDayWithFine;

            var daysWithoutDaysWithFine = days - daysDiference;

            amount = daysWithoutDaysWithFine * selectPlan.Price;

            amount = amount + amountWithDiferenceDays;
        }
        else if (request.FinalDateTime > request.PrevisionFinalDateTime)
        {
            TimeSpan? diference = request.FinalDateTime.Value - request.PrevisionFinalDateTime.Value;
            int daysDiference = diference.Value.Days;

            var pricePerDayWithFine = selectPlan.Price + 50;

            fine = 50;

            var amountWithDiferenceDays = daysDiference * pricePerDayWithFine;

            var daysWithoutDaysWithFine = days - daysDiference;

            amount = daysWithoutDaysWithFine * selectPlan.Price;

            amount = amount + amountWithDiferenceDays;
        }


        var serviceToCreate = new ServiceEntity()
        {
            FinalDateTime = request.FinalDateTime.ToString(),
            Fine = fine,
            Plan = selectPlan,
            Price = selectPlan.Price,
            Amount = amount,
            InitialDateTime = request.InitialDateTime.ToString(),
            ClientEntity = selectClient,
            PrevisionFinalDateTime = request.PrevisionFinalDateTime.ToString()
        };

        var insertService = await _serviceRepository.InsertAsync(serviceToCreate);

        return _mapper.Map<ServiceDto>(insertService);
    }
}
