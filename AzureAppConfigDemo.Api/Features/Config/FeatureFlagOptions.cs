namespace AzureAppConfigDemo.Api.Features.Config;

using System.ComponentModel;

/// <summary>
/// Feature flags.
/// </summary>
public class FeatureFlagOptions
{
    /// <summary>
    /// Gets or sets a value indicating whether <see cref="SampleRadarModule"/> is enabled.
    /// </summary>
    [Description("Enable Sample Radar Module?")]
    public bool SampleRadarModule { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether <see cref="ApplyApril2024TaxRates"/> is enabled.
    /// </summary>
    [Description("Enable April '24 tax rates?")]
    public bool ApplyApril2024TaxRates { get; set; }
}