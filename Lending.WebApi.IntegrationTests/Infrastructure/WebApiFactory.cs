using Lending.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Testcontainers.PostgreSql;

namespace Lending.WebApi.IntegrationTests.Infrastructure;

public class WebApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    public IConfiguration Configuration { get; private set; } = null!;
    private readonly PostgresContainer _dbContainer = new();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(config =>
        {
            PostgreSqlContainer container = (PostgreSqlContainer)_dbContainer.Container;

            var inMemorySettings = new Dictionary<string, string?>()
            {
               { "ConnectionStrings:Database", container.GetConnectionString() },
            };

            Configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .AddJsonFile("appsettings.integration.json")                
                .Build();

            config.AddConfiguration(Configuration);
        });

        // Is be called after the `ConfigureServices` from the Startup
        // which allows you to overwrite the DI with mocked instances
        // Order of execution:
        // 1. builder.ConfigureServices() inside WebApplicationFactory
        // 2. Startup.ConfigureServices() from application code
        // 3. builder.ConfigureTestServices() inside WebApplicationFactory

        builder.ConfigureTestServices(services =>
        {
            services.AddInfrastructure(Configuration);
        });
    }

    public async Task InitializeAsync() => await _dbContainer.InitializeAsync();

    public new async Task DisposeAsync() => await _dbContainer.DisposeAsync();
}
