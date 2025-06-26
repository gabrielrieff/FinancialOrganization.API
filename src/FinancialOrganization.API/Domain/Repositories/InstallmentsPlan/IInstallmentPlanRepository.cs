using FinancialOrganization.API.Domain.Entity;

namespace FinancialOrganization.API.Domain.Repositories.Installments;

public interface IInstallmentPlanRepository
{
    public Task Register(InstallmentPlan entity, CancellationToken cancellationToken);
    public void Update(InstallmentPlan entity, CancellationToken cancellationToken);
}
