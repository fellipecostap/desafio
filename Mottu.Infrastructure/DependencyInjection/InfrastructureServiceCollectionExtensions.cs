using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Desafio.Application.Common.Functions;
using Desafio.Domain.Interfaces.Repository;
using Desafio.Infrastructure.Context;
using Desafio.Infrastructure.Repository;

namespace Desafio.Infrastructure.DependencyInjection;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<ApplicationDbContext>(options =>

        options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("Desafio.WebApi")));

        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IPreRegistrationRepository, PreRegistrationRepository>();
        services.AddScoped<IPreUpdateEmailRepository, PreUpdateEmailRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IMotorcycleRepository, MotorcycleRepository>();
        services.AddScoped<IPlanRepository, PlanRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserTypeRepository, UserTypeRepository>();
        services.AddScoped<CommonFunctions>();

        return services;
    }
}
