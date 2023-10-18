using FluentValidation;

namespace Lending.Presentation.Receivables.CreateReceivables;

public class CreateReceivablesRequestValidator : AbstractValidator<CreateReceivablesRequest>
{
    public CreateReceivablesRequestValidator()
    {
        RuleFor(x => x.Receivables).NotEmpty();
    }
}