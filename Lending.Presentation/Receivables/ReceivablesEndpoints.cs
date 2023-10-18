using Carter;
using Carter.OpenApi;
using Lending.Presentation.Receivables.CreateReceivables;
using Lending.Presentation.Receivables.GetReceivablesStatistics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Lending.Presentation.Receivables;

public class ReceivablesEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var receivables = app.MapGroup("/receivables");

        receivables.MapPost("", CreateReceivablesHandler.Handle)
            .WithName("CreateReceivables")
            .WithTags("CreateReceivables")
            .IncludeInOpenApi();

        receivables.MapGet("/statistics", GetReceivablesStatisticsHandler.Handle)
            .WithName("GetReceivablesStatistics")
            .WithTags("GetReceivablesStatistics")
            .IncludeInOpenApi();
    }
}