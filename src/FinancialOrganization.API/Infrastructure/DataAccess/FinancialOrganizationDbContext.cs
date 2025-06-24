using FinancialOrganization.API.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace FinancialOrganization.API.Infrasctructure.DataAccess;

public class FinancialOrganizationDbContext : DbContext
{
    public FinancialOrganizationDbContext(DbContextOptions<FinancialOrganizationDbContext> options) : base(options)
    { }

    public DbSet<User> Users { get; set; }
    public DbSet<Card> Cards { get; set; }
    public DbSet<Movement> Movements { get; set; }
    public DbSet<InstallmentPlan> InstallmentPlans { get; set; }
    public DbSet<Installment> Installments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Installment>()
            .Property(i => i.Amount)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Movement>()
            .Property(m => m.AmountTotal)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Movement>()
        .HasOne(m => m.InstallmentPlan)
        .WithOne(ip => ip.Movement)
        .HasForeignKey<InstallmentPlan>(ip => ip.MovementId);
    }
}
