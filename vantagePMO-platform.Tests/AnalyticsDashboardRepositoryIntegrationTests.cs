using Microsoft.EntityFrameworkCore;
using vantagePMO_platform.Analytics.Domain.Model.Aggregates;
using vantagePMO_platform.Analytics.Domain.Model.ValueObjects;
using vantagePMO_platform.Analytics.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using vantagePMO_platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

namespace vantagePMO_platform.Tests.Analytics;

public class AnalyticsDashboardRepositoryIntegrationTests
{
    [Fact]
    public async Task AddAndList_ShouldPersistAndReturnAnalyticsDashboard()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        await using var context = new AppDbContext(options);

        var repository = new AnalyticsDashboardRepository(context);

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

        // Act
        await repository.AddAsync(dashboard);
        await context.SaveChangesAsync();

        var result = await repository.ListOrderedAsync();

        // Assert
        Assert.Single(result);
        Assert.Equal(72, result[0].PortfolioRoi.Percentage);
        Assert.Equal("HIGH EFFICIENCY", result[0].PortfolioRoi.EfficiencyLabel);
        Assert.Equal(1_450_000, result[0].SummaryKpis.UnallocatedFunds);
        Assert.Equal("Engineering", result[0].ResourceSaturation[0].Department);
    }
}