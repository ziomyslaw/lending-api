using Marten;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lending.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMarten(options =>
        {
            var connectionString = configuration.GetConnectionString("Database")!;
            options.Connection(connectionString);
        });
        return services;
    }
}
