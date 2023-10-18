using Lending.Application.Abstractions.Messaging;
using Lending.Domain.Receivables;
using Marten;

namespace Lending.Application.Receivables.GetReceivablesStatistics;

public sealed class GetReceivablesStatisticsQueryHandler : IQueryHandler<GetReceivablesStatisticsQuery, ReceivablesStatistics>
{
    private readonly IQuerySession _session;

    public GetReceivablesStatisticsQueryHandler(IQuerySession session)
    {
        _session = session;
    }

    public async Task<ReceivablesStatistics> Handle(GetReceivablesStatisticsQuery request, CancellationToken cancellationToken)
    {
        var nonCancelledInvoices = await _session.Query<Receivable>().LongCountAsync(i => i.Cancelled != true, cancellationToken);
        var openInvoices = await _session.Query<Receivable>().LongCountAsync(i => i.Cancelled != true && i.ClosedDate == "" || i.ClosedDate == null, cancellationToken);
        var closedInvoices = nonCancelledInvoices - openInvoices;

        return new ReceivablesStatistics(openInvoices, closedInvoices);
    }
}
