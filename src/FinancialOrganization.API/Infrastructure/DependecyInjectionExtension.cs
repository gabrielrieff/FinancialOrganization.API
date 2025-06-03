using FinancialOrganization.API.Domain.Repositories;
using FinancialOrganization.API.Domain.Repositories.Cards;
using FinancialOrganization.API.Domain.Repositories.Installments;
using FinancialOrganization.API.Domain.Repositories.Movements;
using FinancialOrganization.API.Infrasctructure.DataAccess;
using FinancialOrganization.API.Infrastructure.DataAccess;
using FinancialOrganization.API.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FinancialOrganization.API.Infrasctructure;

public static class DependecyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddRepositories(services);
        AddDbContext(services, configuration);
    }
    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        //card
        services.AddScoped<ICardRepository, CardRepositories>();

        //Movement
        services.AddScoped<IMovementRepository, MovementRepositories>();
        services.AddScoped<IInstallmentPlanRepository, InstallmentPlanRepositories>();
        services.AddScoped<IInstallmentRepository, InstallmentRepostories>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {

        var connectionString = configuration.GetConnectionString("connection");

        services.AddDbContext<FinancialOrganizationDbContext>(config => 
            config.UseSqlServer(connectionString));
    }
}
