namespace AzureAppConfigDemo.Api.Features.Config;

using System.ComponentModel;

/// <summary>
/// Feature flags.
/// </summary>
public class FeatureFlagOptions
{
    /// <summary>
    /// Gets or sets a value indicating whether <see cref="SampleApp1Module"/> is enabled.
    /// </summary>
    [Description("Enable Sample App1 Module?")]
    public bool SampleApp1Module { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether <see cref="ApplyApril2024TaxRates"/> is enabled.
    /// </summary>
    [Description("Enable April '24 tax rates?")]
    public bool ApplyApril2024TaxRates { get; set; }
}