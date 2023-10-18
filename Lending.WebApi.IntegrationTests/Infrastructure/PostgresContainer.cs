using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Testcontainers.PostgreSql;

namespace Lending.WebApi.IntegrationTests.Infrastructure;

public class PostgresContainer : ContainerFixture
{
    private const string database = "lending";
    private const string username = "postgres";
    private const string password = "postgres";
    private const string dockerHost = nameof(dockerHost);

    protected override IContainer BuildContainer() => 
        new PostgreSqlBuilder() // new ContainerBuilder()
        .WithImage("postgres:latest")
        .WithDatabase(database) // .WithEnvironment("POSTGRES_DB", database)
        .WithUsername(username) // .WithEnvironment("POSTGRES_USER", username)
        .WithPassword(password) // .WithEnvironment("POSTGRES_PASSWORD", password)
        .WithPortBinding(5432, assignRandomHostPort: true)
        .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(5432))
        .WithCleanUp(true)
        .Build();
}
