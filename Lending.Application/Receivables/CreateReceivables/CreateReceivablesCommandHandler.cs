using Lending.Application.Abstractions.Messaging;
using Marten;

namespace Lending.Application.Receivables.CreateReceivables;

public sealed class CreateReceivablesCommandHandler : ICommandHandler<CreateReceivablesCommand>
{
    private readonly IDocumentStore _store;

    public CreateReceivablesCommandHandler(IDocumentStore store)
    {
        _store = store;
    }

    public async Task Handle(CreateReceivablesCommand request, CancellationToken cancellationToken)
    {
        await _store.BulkInsertAsync(request.Receivables, cancellation: cancellationToken);
    }
}
