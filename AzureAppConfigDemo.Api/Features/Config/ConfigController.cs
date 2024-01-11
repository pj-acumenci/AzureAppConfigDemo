namespace AzureAppConfigDemo.Api.Features.Config;

using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.Options;

/// <summary>
/// Config controller.
/// </summary>
/// <param name="appSettings">The app settings.</param>
/// <param name="globalSettings">The global settings.</param>
/// <param name="featureFlags">The feature flags.</param>
/// <param name="refresherProvider">The configuration refresher provider.</param>
[ApiController]
[AllowAnonymous]
[Route("[controller]")]
public class ConfigController(
    IOptionsSnapshot<App1Settings> appSettings,
    IOptionsSnapshot<GlobalSettings> globalSettings,
    IOptionsSnapshot<FeatureFlagOptions> featureFlags,
    IConfigurationRefresherProvider refresherProvider)
{
    private readonly App1Settings appSettings = appSettings.Value;
    private readonly GlobalSettings globalSettings = globalSettings.Value;
    private readonly FeatureFlagOptions featureFlagOptions = featureFlags.Value;

    /// <summary>
    /// Gets the app settings.
    /// </summary>
    /// <returns>The app settings.</returns>
    [HttpGet]
    [Route("settings")]
    public App1Settings GetAppSettings() => this.appSettings;

    /// <summary>
    /// Gets the global settings.
    /// </summary>
    /// <returns>The global settings.</returns>
    [HttpGet]
    [Route("global/settings")]
    public GlobalSettings GetGlobalSettings() => this.globalSettings;

    /// <summary>
    /// Gets feature flag information.
    /// </summary>
    /// <returns>Feature flag information.</returns>
    [HttpGet]
    [Route("features")]
    public Dictionary<string, FeatureWebModel> GetFeatures()
    {
        return this.featureFlagOptions == null ? null! : typeof(FeatureFlagOptions)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty)
            .ToDictionary(prop => prop.Name, prop => new FeatureWebModel(
                prop.Name,
                prop.GetCustomAttribute<DescriptionAttribute>()?.Description,
                (bool)prop.GetValue(this.featureFlagOptions)!));
    }

    /// <summary>
    /// Refreshes all configuration.
    /// </summary>
    /// <returns>Async task.</returns>
    [HttpPost]
    [Route("forceRefresh")]
    public void ForceRefresh()
    {
        refresherProvider.Refreshers
            .Where(r => r.GetType().Name == "AzureAppConfigurationProvider")
            .OfType<ConfigurationProvider>()
            .FirstOrDefault()?.Load();
    }
}
