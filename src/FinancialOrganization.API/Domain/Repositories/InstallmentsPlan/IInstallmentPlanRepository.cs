using FinancialOrganization.API.Domain.Entity;
using FinancialOrganization.API.Domain.SeedWork.SearchableRepository;

namespace FinancialOrganization.API.Domain.Repositories.Installments;

public interface IInstallmentPlanRepository : IGenegicRepository<InstallmentPlan>, ISearchableRepository<InstallmentPlan>
{
}
