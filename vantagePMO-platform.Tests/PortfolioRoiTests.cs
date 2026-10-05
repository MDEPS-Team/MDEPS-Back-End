using vantagePMO_platform.Analytics.Domain.Model.ValueObjects;

namespace vantagePMO_platform.Tests.Analytics;

public class PortfolioRoiTests
{
    [Fact]
    public void Constructor_ShouldAssignProvidedValues()
    {
        // Arrange
        var percentage = 72;
        var efficiencyLabel = "HIGH EFFICIENCY";
        var target = 12_400_000L;
        var projected = 14_100_000L;

        // Act
        var portfolioRoi = new PortfolioRoi(
            percentage,
            efficiencyLabel,
            target,
            projected);

        // Assert
        Assert.Equal(percentage, portfolioRoi.Percentage);
        Assert.Equal(efficiencyLabel, portfolioRoi.EfficiencyLabel);
        Assert.Equal(target, portfolioRoi.Target);
        Assert.Equal(projected, portfolioRoi.Projected);
    }
}