using Microsoft.Extensions.Configuration;
using Respawn;
using Respawn.Graph;

namespace Lending.WebApi.IntegrationTests.Infrastructure;

[Trait("Category", "Integration")]
public abstract class BaseIntegrationTest : IClassFixture<WebApiFactory>
{
    private readonly RespawnerOptions _checkpoint = new()
    {
        SchemasToInclude = new[]
        {
            ""
        },
        TablesToIgnore = new Table[]
        {
            "sysdiagrams",
            "tblUser",
            "tblObjectType",
        },
        WithReseed = false
    };

    protected readonly WebApiFactory _factory;
    protected readonly HttpClient _client;

    public BaseIntegrationTest(WebApiFactory fixture)
    {
        _factory = fixture;
        _client = _factory.CreateClient();

        const bool isResetDbEnabled = false; // turn off, change this if you need to reset db
        ResetDatabase(isResetDbEnabled).GetAwaiter().GetResult();
    }

    async Task ResetDatabase(bool isEnabled)
    {
        if (!isEnabled) return;
        string connection = _factory.Configuration.GetConnectionString("Database")!;
        var respawner = await Respawner.CreateAsync(connection, _checkpoint);
        await respawner.ResetAsync(connection);
    }
}
