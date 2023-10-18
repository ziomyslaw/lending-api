using Carter.ModelBinding;
using Lending.Application.Receivables.CreateReceivables;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Lending.Presentation.Receivables.CreateReceivables;

internal class CreateReceivablesHandler
{
    public static async Task<IResult> Handle(HttpContext context, CreateReceivablesRequest request, ISender sender)
    {
        var result = context.Request.Validate(request);
        if (!result.IsValid)
        {
            return Results.UnprocessableEntity(result.GetFormattedErrors());
        }

        var command = request.Adapt<CreateReceivablesCommand>();
        await sender.Send(command);
        return Results.Ok();
    }
}
