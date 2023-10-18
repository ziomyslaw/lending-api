using Bogus;
using FluentAssertions;
using Lending.Domain.Receivables;
using Lending.Presentation.Receivables.CreateReceivables;
using Lending.WebApi.IntegrationTests.Infrastructure;
using System.Net;
using System.Net.Http.Json;

namespace Lending.WebApi.IntegrationTests;

public class ReceivablesTests : BaseIntegrationTest
{
    public ReceivablesTests(WebApiFactory application) : base(application) { }

    [Fact]
    public async Task Receivables_GetStatistics_NoData_ReturnsOk()
    {
        // arrange 

        // act
        var response = await _client.GetAsync("/receivables/statistics");
        var summary = await response.Content.ReadFromJsonAsync<ReceivablesStatistics>();

        // assert
        response.EnsureSuccessStatusCode();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        summary.Should().NotBeNull();
    }

    [Fact]
    public async Task Receivables_GetStatistics_RandomData_ReturnsOk()
    {
        // arrange
        Faker<ReceivableDto> generator = new Faker<ReceivableDto>()
            .UseSeed(1000)
            .RuleFor(o => o.PaidValue, f => f.Random.Decimal(0, 10))
            .RuleFor(o => o.OpeningValue, f => f.Random.Decimal(0, 10))
            .RuleFor(o => o.ClosedDate, f => f.Date.Past().ToString());

        var cancelledReceivables = generator.GenerateBetween(100, 200).Select(i => i with { Cancelled = true }).ToList();
        var openReceivables = generator.GenerateBetween(100, 200).Select(i => i with { Cancelled = false, ClosedDate = null }).ToList();
        var closedReceivables = generator.GenerateBetween(100, 200).Select(i => i with { Cancelled = false }).ToList();
        var allReceivables = cancelledReceivables.Concat(openReceivables).Concat(closedReceivables).ToList();

        CreateReceivablesRequest request = new(allReceivables);
        HttpContent content = JsonContent.Create(request);

        // act
        await _client.PostAsync("/receivables", content);
        var response = await _client.GetAsync("/receivables/statistics");
        var summary = await response.Content.ReadFromJsonAsync<ReceivablesStatistics>();

        // assert
        response.EnsureSuccessStatusCode();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        summary.Should().NotBeNull();
        summary?.OpenInvoices.Should().Be(openReceivables.Count);
        summary?.ClosedInvoices.Should().Be(closedReceivables.Count);
    }

    [Theory]
    [InlineData("ref1", "EUR", "2023-10-10", 123.45, 1.0, "2023-11-10", "2023-10-13", false, "debtorName", "debtorReference1", "debtorAddress1", "debtorAddress2", "debtorTown", "debtorState", "debtorZip", "debtorCountryCode", "debtorRegistrationNumber")]
    [InlineData("ref2", "EUR", "2023-10-10", 123.45, 2.0, "2023-11-10", null, false, "debtorName", "debtorReference2", null, null, null, null, null, "debtorCountryCode", null)]
    [InlineData("ref3", "EUR", "2023-10-10", 123.45, 2.0, "2023-11-10", null, null, "debtorName", "debtorReference3", null, null, null, null, null, "debtorCountryCode", null)]
    [InlineData("ref4", "EUR", "2023-10-10", 0, 0, "2023-11-10", "2023-10-13", true, "debtorName", "debtorReference1", "debtorAddress1", "debtorAddress2", "debtorTown", "debtorState", "debtorZip", "debtorCountryCode", "debtorRegistrationNumber")]
    public async Task Receivables_Create_SingleItem_ReturnsOk(
        string reference,
        string currencyCode,
        string issueDate,
        decimal openingValue,
        decimal paidValue,
        string dueDate,
        string? closedDate,
        bool? cancelled,
        string debtorName,
        string debtorReference,
        string? debtorAddress1,
        string? debtorAddress2,
        string? debtorTown,
        string? debtorState,
        string? debtorZip,
        string debtorCountryCode,
        string? debtorRegistrationNumber)
    {
        // arrange 
        var receivable = new ReceivableDto()
        {
            Reference = reference,
            CurrencyCode = currencyCode,
            IssueDate = issueDate,
            OpeningValue = openingValue,
            PaidValue = paidValue,
            DueDate = dueDate,
            ClosedDate = closedDate,
            Cancelled = cancelled,
            DebtorName = debtorName,
            DebtorReference = debtorReference,
            DebtorAddress1 = debtorAddress1,
            DebtorAddress2 = debtorAddress2,
            DebtorTown = debtorTown,
            DebtorState = debtorState,
            DebtorZip = debtorZip,
            DebtorCountryCode = debtorCountryCode,
            DebtorRegistrationNumber = debtorRegistrationNumber
        };
        CreateReceivablesRequest request = new(new List<ReceivableDto> { receivable });
        HttpContent content = JsonContent.Create(request);

        // act
        var response = await _client.PostAsync("/receivables", content);

        // assert
        response.EnsureSuccessStatusCode();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Receivables_Create_BatchItems_ReturnsOk()
    {
        // arrange 
        Faker<ReceivableDto> generator = new Faker<ReceivableDto>().UseSeed(1000);
        CreateReceivablesRequest request = new(generator.GenerateBetween(100,500));
        HttpContent content = JsonContent.Create(request);

        // act
        var response = await _client.PostAsync("/receivables", content);

        // assert
        response.EnsureSuccessStatusCode();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Receivables_Create_EmptyList_ReturnsUnprocessable()
    {
        // arrange 
        CreateReceivablesRequest request = new(new List<ReceivableDto>());
        HttpContent content = JsonContent.Create(request);

        // act
        var response = await _client.PostAsync("/receivables", content);
        var error = await response.Content.ReadAsStringAsync();

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        error.Should().NotBeNullOrEmpty();
    }
}