using FinancialOrganization.API.Application.UseCase.Cards.Delete;
using FinancialOrganization.API.Application.UseCase.Cards.GetAll;
using FinancialOrganization.API.Application.UseCase.Cards.ListAll;
using FinancialOrganization.API.Application.UseCase.Cards.Register;
using FinancialOrganization.API.Application.UseCase.Cards.Update;
using FinancialOrganization.API.Application.UseCase.Movements.ListAll;
using FinancialOrganization.API.Application.UseCase.Movements.Register;
using FinancialOrganization.API.Application.UseCase.Movements.SearchList;
using FinancialOrganization.API.Application.UseCase.Movements.UpdateAmount;
using FinancialOrganization.API.Application.UseCase.Movements.UpdateStatus;

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
        services.AddScoped<IUpdateCardUseCase, UpdateCardUseCase>();
        services.AddScoped<ISearchListUseCase, SearchListUseCase>();
        services.AddScoped<IDeleteCardUseCase, DeleteCardUseCase>();
        services.AddScoped<IListAllUseCase, ListAllUseCase>();

        //Movement
        services.AddScoped<IRegisterMovementUseCase, RegisterMovementUseCase>();
        services.AddScoped<IListAllMovementsUseCase, ListAllMovementsUseCase>();
        services.AddScoped<ISearchListMovementUseCase, SearchListMovementUseCase>();

        //Installment
        services.AddScoped<IUpdateAmountMovementUseCase, UpdateAmountMovementUseCase>();
        services.AddScoped<IUpdateStatusMovementUseCase, UpdateStatusMovementUseCase>();

    }
}
