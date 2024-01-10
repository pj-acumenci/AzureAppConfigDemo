namespace AzureAppConfigDemo.Api.Features.Config;

using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

/// <summary>
/// Config controller.
/// </summary>
/// <param name="settings">The settings.</param>
/// <param name="featureFlags">The feature flags.</param>
[ApiController]
[AllowAnonymous]
[Route("[controller]")]
public class ConfigController(
    IOptionsSnapshot<App1Settings> settings,
    IOptionsSnapshot<FeatureFlagOptions> featureFlags)
{
    private readonly App1Settings settings = settings.Value;
    private readonly FeatureFlagOptions featureFlagOptions = featureFlags.Value;

    /// <summary>
    /// Gets settings.
    /// </summary>
    /// <returns>The settings.</returns>
    [HttpGet]
    [Route("settings")]
    public App1Settings GetSettings() => this.settings;

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
}
