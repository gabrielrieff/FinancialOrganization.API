using FinancialOrganization.API.Domain.Repositories;
using FinancialOrganization.API.Domain.Repositories.Cards;
using FinancialOrganization.API.Domain.Repositories.Installments;
using FinancialOrganization.API.Domain.Repositories.Movements;
using FinancialOrganization.API.Domain.Repositories.Users;
using FinancialOrganization.API.Domain.Security.Cryptography;
using FinancialOrganization.API.Domain.Security.Tokens;
using FinancialOrganization.API.Domain.Services.LoggedUser;
using FinancialOrganization.API.Infrasctructure.DataAccess;
using FinancialOrganization.API.Infrastructure.DataAccess;
using FinancialOrganization.API.Infrastructure.DataAccess.Repositories;
using FinancialOrganization.API.Infrastructure.Security.Criptography;
using FinancialOrganization.API.Infrastructure.Security.Tokens;
using FinancialOrganization.API.Infrastructure.Services.LoggedUser;
using Microsoft.EntityFrameworkCore;

namespace FinancialOrganization.API.Infrasctructure;

public static class DependecyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        AddRepositories(services);
        AddDbContext(services, configuration);
        AddToken(services, configuration);

        services.AddScoped<IPasswordEncripter, Encripty>();
        services.AddScoped<ILoggedUser, LoggedUser>();
    }

    private static void AddToken(IServiceCollection services, IConfiguration configuration)
    {
        var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");
        var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpirationTimeMinutes");

        services.AddScoped<IAccessTokenGenerator>(provider =>
            new JwtGenerator(expirationTimeMinutes, signingKey!));
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        //card
        services.AddScoped<ICardRepository, CardRepositories>();
        
        //user
        services.AddScoped<IUserRepository, UserRepositories>();

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
