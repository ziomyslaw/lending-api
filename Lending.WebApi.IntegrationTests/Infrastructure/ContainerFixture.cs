using DotNet.Testcontainers.Containers;

namespace Lending.WebApi.IntegrationTests.Infrastructure;

public abstract class ContainerFixture : IAsyncLifetime
{
    public IContainer Container { get; }

    protected ContainerFixture()
    {
        Container = BuildContainer();
    }

    protected abstract IContainer BuildContainer();    

    public virtual async Task InitializeAsync() => await Container.StartAsync();

    public virtual async Task DisposeAsync()
    {
        // Clean up our container nicely.
        if (Container is not null)
        {
            await Container.StopAsync();
            await Container.DisposeAsync();
        }
    }
}
