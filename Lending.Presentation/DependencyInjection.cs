using Carter;
using Microsoft.Extensions.DependencyInjection;

namespace Lending.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services) => services.AddCarter();
}
