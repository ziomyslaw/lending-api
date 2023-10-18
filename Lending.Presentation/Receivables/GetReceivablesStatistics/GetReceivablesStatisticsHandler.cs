using Lending.Application.Receivables.GetReceivablesStatistics;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Lending.Presentation.Receivables.GetReceivablesStatistics;

internal class GetReceivablesStatisticsHandler
{
    public static async Task<IResult> Handle(ISender sender)
    {
        var command = new GetReceivablesStatisticsQuery();
        var result = await sender.Send(command);
        return Results.Ok(result);
    }
}
