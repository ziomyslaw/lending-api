using Carter;
using Microsoft.AspNetCore.Routing;

namespace Lending.Presentation;

public static class MiddlewareExtentions
{
    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder builder) => builder.MapCarter();
}
