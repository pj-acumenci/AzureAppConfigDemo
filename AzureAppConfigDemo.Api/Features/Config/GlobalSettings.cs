namespace AzureAppConfigDemo.Api.Features.Config;

/// <summary>
/// Settings for all apps.
/// </summary>
public class GlobalSettings
{
    /// <summary>
    /// Gets the current UK VAT rate.
    /// </summary>
    public double? UKVATRate { get; set; }
}
