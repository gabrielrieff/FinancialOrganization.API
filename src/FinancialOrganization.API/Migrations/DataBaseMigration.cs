using FinancialOrganization.API.Infrasctructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace FinancialOrganization.API.Migrations;

public static class DataBaseMigration
{
    public static async Task MigrateDataBase(IServiceProvider serviceProvider)
    {
        var dbContext = serviceProvider.GetRequiredService<FinancialOrganizationDbContext>();

        await dbContext.Database.MigrateAsync();
    }
}
