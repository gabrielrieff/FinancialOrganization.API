using FinancialOrganization.API.Application.UseCase.Cards.Register;

namespace FinancialOrganization.API.Application;

public static class DependecyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddUseCases(services);
    }
    private static void AddUseCases(IServiceCollection services)
    {

        //Card
        services.AddScoped<IRegisterCardUseCase, RegisterCardUseCase>();


    }
}
