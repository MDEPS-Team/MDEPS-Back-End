using Microsoft.EntityFrameworkCore;
using Reqnroll;
using vantagePMO_platform.Analytics.Domain.Model.Aggregates;
using vantagePMO_platform.Analytics.Domain.Model.ValueObjects;
using vantagePMO_platform.Analytics.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using vantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

namespace vantagePMO_platform.Tests.Features.Steps;

[Binding]
public class AnalyticsDashboardSteps
{
    private AppDbContext? _context;
    private AnalyticsDashboardRepository? _repository;
    private IReadOnlyList<AnalyticsDashboard>? _result;

    [Given("that analytics information exists for the portfolio")]
    public async Task GivenAnalyticsInformationExistsForThePortfolio()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new AppDbContext(options);
        _repository = new AnalyticsDashboardRepository(_context);

        var dashboard = new AnalyticsDashboard(
            new List<MonthlyExpenditure>
            {
                new("JAN", 62, 55)
            },
            new PortfolioRoi(
                72,
                "HIGH EFFICIENCY",
                12_400_000,
                14_100_000),
            new List<DepartmentCapacity>
            {
                new("Engineering", 94, "warning")
            },
            new List<TopMover>
            {
                new(
                    "PX-882",
                    "Cloud Migration",
                    "Infrastructure",
                    14.2,
                    "Optimal")
            },
            new SummaryKpis(
                1_450_000,
                "Available for Q3 expansion",
                1.4,
                12,
                "Low",
                3));

        await _repository.AddAsync(dashboard);
        await _context.SaveChangesAsync();
    }

    [When("the Project Manager requests the analytics dashboard")]
    public async Task WhenTheProjectManagerRequestsTheAnalyticsDashboard()
    {
        _result = await _repository!.ListOrderedAsync();
    }

    [Then("the system returns the portfolio performance indicators")]
    public void ThenTheSystemReturnsThePortfolioPerformanceIndicators()
    {
        Assert.NotNull(_result);
        Assert.NotEmpty(_result);

        var dashboard = _result[0];

        Assert.Equal(72, dashboard.PortfolioRoi.Percentage);
        Assert.Equal("HIGH EFFICIENCY", dashboard.PortfolioRoi.EfficiencyLabel);
        Assert.Equal(1_450_000, dashboard.SummaryKpis.UnallocatedFunds);
    }
}