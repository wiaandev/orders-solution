using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace assessment.Infrastructure.Database;

// created this here so that I can run migrations and add them in infrastructure
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var connectionString =
            Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
            ?? throw new InvalidOperationException(
                "Connection string is missing");

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlServer(connectionString, sql =>
        {
            sql.MigrationsAssembly("assessment.Infrastructure");
            sql.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            sql.CommandTimeout(600);
        });

        return new AppDbContext(optionsBuilder.Options);
    }
}
