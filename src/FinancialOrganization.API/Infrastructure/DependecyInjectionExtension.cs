using FinancialOrganization.API.Domain.Repositories;
using FinancialOrganization.API.Domain.Repositories.Cards;
using FinancialOrganization.API.Infrasctructure.DataAccess;
using FinancialOrganization.API.Infrastructure.DataAccess;
using FinancialOrganization.API.Infrastructure.DataAccess.Repositories.Cards;
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
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {

        var connectionString = configuration.GetConnectionString("connection");

        services.AddDbContext<FinancialOrganizationDbContext>(config => 
            config.UseSqlServer(connectionString, 
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                }));
    }
}
