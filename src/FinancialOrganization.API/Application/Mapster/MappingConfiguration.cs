using FinancialOrganization.API.Communication.DTOs;
using FinancialOrganization.API.Domain.Entity;
using Mapster;

namespace FinancialOrganization.API.Application.MappingConfiguration;

public class MappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {

        config.NewConfig<Movement, MovementDto>()
            .Map(dest => dest.InstallmentPlan, 
                src => src.InstallmentPlan.Adapt<InstallmentDto>())
            .Map(dest => dest.Card, src => src.Card)
            .Map(
                dest => dest.InstallmentPlan.Installments,
                src => src.InstallmentPlan.Installments)
            .MapToConstructor(true);

        config.NewConfig<InstallmentPlan, InstallmentPlanDto>()
            .Map(
                dest => dest.Installments,
                src => src.Installments.Adapt<InstallmentPlanDto>())
            .MapToConstructor(true);
        //config.NewConfig<Installment, InstallmentDto>();
        config.NewConfig<Card, CardDto>();
    }
}