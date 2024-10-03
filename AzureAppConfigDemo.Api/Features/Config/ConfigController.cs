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
/// <param name="bareConfig">The configuration.</param>
/// <param name="appSettings">The app settings.</param>
/// <param name="globalSettingsFromIOptions">The global settings (from IOptions).</param>
/// <param name="globalSettingsFromSnapshot">The global settings (from IOptionsSnapshot).</param>
/// <param name="featureFlags">The feature flags.</param>
/// <param name="refresherProvider">The configuration refresher provider.</param>
[ApiController]
[AllowAnonymous]
[Route("[controller]")]
public class ConfigController(
    IConfiguration bareConfig,
    IOptionsSnapshot<App1Settings> appSettings,
    IOptions<GlobalSettings> globalSettingsFromIOptions,
    IOptionsSnapshot<GlobalSettings> globalSettingsFromSnapshot,
    IOptionsSnapshot<FeatureFlagOptions> featureFlags,
    IConfigurationRefresherProvider refresherProvider)
{
    /// <summary>
    /// Gets the app settings.
    /// </summary>
    /// <returns>The app settings.</returns>
    [HttpGet]
    [Route("settings")]
    public App1Settings GetAppSettings() => appSettings.Value;

    /// <summary>
    /// Gets the global settings.
    /// </summary>
    /// <returns>The global settings.</returns>
    [HttpGet]
    [Route("global/settings-from-iconfig")]
    public GlobalSettings GetGlobalSettingsFromIConfig()
    {
        var retVal = new GlobalSettings();
        bareConfig.GetSection("Global").Bind(retVal);
        return retVal;
    }

    /// <summary>
    /// Gets the global settings.
    /// </summary>
    /// <returns>The global settings.</returns>
    [HttpGet]
    [Route("global/settings-from-ioptions")]
    public GlobalSettings GetGlobalSettingsNoSnaphot() => globalSettingsFromIOptions.Value;

    /// <summary>
    /// Gets the global settings.
    /// </summary>
    /// <returns>The global settings.</returns>
    [HttpGet]
    [Route("global/settings-from-snapshot")]
    public GlobalSettings GetGlobalSettings() => globalSettingsFromSnapshot.Value;

    /// <summary>
    /// Gets feature flag information.
    /// </summary>
    /// <returns>Feature flag information.</returns>
    [HttpGet]
    [Route("features")]
    public Dictionary<string, FeatureWebModel> GetFeatures()
    {
        return featureFlags.Value == null ? null! : typeof(FeatureFlagOptions)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty)
            .ToDictionary(prop => prop.Name, prop => new FeatureWebModel(
                prop.Name,
                prop.GetCustomAttribute<DescriptionAttribute>()?.Description,
                (bool)prop.GetValue(featureFlags.Value)!));
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
