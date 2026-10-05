using vantagePMO_platform.Analytics.Domain.Model.ValueObjects;

namespace vantagePMO_platform.Tests.Analytics;

public class SummaryKpisTests
{
    [Fact]
    public void Constructor_ShouldAssignProvidedValues()
    {
        // Arrange
        var unallocatedFunds = 1_450_000L;
        var unallocatedSubtext = "Available for Q3 expansion";
        var velocityIndex = 1.4;
        var velocityChange = 12;
        var riskExposure = "Low";
        var mitigationPlans = 3;

        // Act
        var summaryKpis = new SummaryKpis(
            unallocatedFunds,
            unallocatedSubtext,
            velocityIndex,
            velocityChange,
            riskExposure,
            mitigationPlans);

        // Assert
        Assert.Equal(unallocatedFunds, summaryKpis.UnallocatedFunds);
        Assert.Equal(unallocatedSubtext, summaryKpis.UnallocatedSubtext);
        Assert.Equal(velocityIndex, summaryKpis.VelocityIndex);
        Assert.Equal(velocityChange, summaryKpis.VelocityChange);
        Assert.Equal(riskExposure, summaryKpis.RiskExposure);
        Assert.Equal(mitigationPlans, summaryKpis.MitigationPlans);
    }
}