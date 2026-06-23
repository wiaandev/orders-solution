using assessment.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace assessment.Infrastructure.Bootstrap;

public class Bootstrap(AppDbContext appDbContext, SeedService seedService)
{
    public async Task DropDatabaseAsync()
    {
        await appDbContext.Database.EnsureDeletedAsync();
    }

    public async Task MigrateDatabaseAsync()
    {
        await appDbContext.Database.MigrateAsync();
    }

    public async Task SeedDatabaseAsync(CancellationToken ct = default)
    {
        await seedService.SeedAsync(ct);
    }
}