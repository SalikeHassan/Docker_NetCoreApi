using Microsoft.EntityFrameworkCore;
using TestProject.ZipPay.Infrastructure.Context;

namespace TestProject.ZipPay.Api
{
    public static class DatabaseManagementService
    {
        // Getting the scope of our database context
        public static void MigrationInitialisation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                // Takes all of our migrations files and apply them against the database in case they are not applied
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ZipPayContext>();
                if (dbContext.Database.IsSqlServer())
                {
                    dbContext.Database.Migrate();
                }
            }
        }
    }
}
