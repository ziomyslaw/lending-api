namespace Lending.Domain.Receivables;

public class Receivable
{
    public long Id { get; set; }
    public required string Reference { get; set; }
    public required string CurrencyCode { get; set; }
    public required string IssueDate { get; set; }
    public decimal OpeningValue { get; set; }
    public decimal PaidValue { get; set; }
    public required string DueDate { get; set; }
    public string? ClosedDate { get; set; }
    public bool? Cancelled { get; set; }
    public required string DebtorName { get; set; }
    public required string DebtorReference { get; set; }
    public string? DebtorAddress1 { get; set; }
    public string? DebtorAddress2 { get; set; }
    public string? DebtorTown { get; set; }
    public string? DebtorState { get; set; }
    public string? DebtorZip { get; set; }
    public required string DebtorCountryCode { get; set; }
    public string? DebtorRegistrationNumber { get; set; }
}
