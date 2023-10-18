using Lending.Application.Abstractions.Messaging;
using Lending.Domain.Receivables;

namespace Lending.Application.Receivables.CreateReceivables;

public sealed record CreateReceivablesCommand(List<Receivable> Receivables) : ICommand;
