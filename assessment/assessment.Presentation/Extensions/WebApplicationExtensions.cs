using assessment.Infrastructure.Bootstrap;
using Microsoft.Extensions.Options;
using BootstrapService = assessment.Infrastructure.Bootstrap.Bootstrap;

namespace assessment.Presentation.Extensions;

public static class WebApplicationExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();
        var sp = scope.ServiceProvider;

        var bootstrap = sp.GetRequiredService<BootstrapService>();
        var dropOptions = sp.GetRequiredService<IOptions<DropOptions>>().Value;
        var migrateOptions = sp.GetRequiredService<IOptions<MigrateOptions>>().Value;
        var seedOptions = sp.GetRequiredService<IOptions<SeedOptions>>().Value;

        if (dropOptions.Enabled)
            await bootstrap.DropDatabaseAsync();

        if (migrateOptions.Enabled)
            await bootstrap.MigrateDatabaseAsync();

        if (seedOptions.Enabled)
            await bootstrap.SeedDatabaseAsync();
    }
}
