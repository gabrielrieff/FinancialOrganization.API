using FinancialOrganization.API.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace FinancialOrganization.API.Infrasctructure.DataAccess;

public class FinancialOrganizationDbContext : DbContext
{
    public DbSet<Card> Cards { get; set; }

    public FinancialOrganizationDbContext(DbContextOptions<FinancialOrganizationDbContext> options) : base(options)
    { }
}
