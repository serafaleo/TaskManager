using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.Api.Core.Migration;

public class MigrationContextFactory : IDesignTimeDbContextFactory<MigrationContext>
{
    public MigrationContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
#if DEBUG
            .AddJsonFile("appsettings.Development.json")
#else
            .AddJsonFile("appsettings.json")
#endif
            .Build();

        DbContextOptionsBuilder<MigrationContext> optionsBuilder = new DbContextOptionsBuilder<MigrationContext>();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("SQLServerConnectionString"));

        return new MigrationContext(optionsBuilder.Options);
    }
}