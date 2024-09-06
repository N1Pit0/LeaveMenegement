using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HR.LeaveManagement.Persistence.DatabaseContext;

public class HrDatabaseFactory : IDesignTimeDbContextFactory<HrDatabaseContext>
{
    public HrDatabaseContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();


        var optionsBuilder = new DbContextOptionsBuilder<HrDatabaseContext>();

        var connectionString = configuration
            .GetConnectionString("HrDatabaseConnectionString");

        optionsBuilder.UseSqlServer(connectionString);

        return new HrDatabaseContext(optionsBuilder.Options);
    }
}