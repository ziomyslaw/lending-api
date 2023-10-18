using Lending.Application.Abstractions.Messaging;
using Lending.Domain.Receivables;

namespace Lending.Application.Receivables.GetReceivablesStatistics;

public sealed record GetReceivablesStatisticsQuery() : IQuery<ReceivablesStatistics>;