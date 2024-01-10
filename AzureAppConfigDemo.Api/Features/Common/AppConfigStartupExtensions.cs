namespace AzureAppConfigDemo.Api.Features.Common;

using AzureAppConfigDemo.Api.Features.Config;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.FeatureManagement;

/// <summary>
/// Extensions for configuring Azure App Config at startup.
/// </summary>
public static class AppConfigStartupExtensions
{
    private const string AppLabel = "radar";

    /// <summary>
    /// Adds the Azure App Config feature.
    /// </summary>
    /// <param name="appBuilder">The host builder.</param>
    public static void AddAppConfigFeature(this WebApplicationBuilder appBuilder)
    {
        var config = appBuilder.Configuration;
        var appConfigConnection = appBuilder.Configuration.GetConnectionString("AppConfig");
        if (!string.IsNullOrEmpty(appConfigConnection) )
        {
            appBuilder.Configuration.AddAzureAppConfiguration(o => o.Connect(appConfigConnection)
                .UseFeatureFlags(o => o
                    .Select(KeyFilter.Any, LabelFilter.Null)
                    .Select(KeyFilter.Any, AppLabel)
                    .CacheExpirationInterval = TimeSpan.FromSeconds(5))
                .Select(KeyFilter.Any, LabelFilter.Null)
                .Select(KeyFilter.Any, AppLabel)
                .ConfigureRefresh(o => o
                    .Register("Radar:Sentinel", true)
                    .SetCacheExpiration(TimeSpan.FromSeconds(5))));
        }

        appBuilder.Services
            .Configure<RadarSettings>(config.GetSection("Radar"))
            .Configure<FeatureFlagOptions>(config.GetSection("FeatureManagement"))
            .AddAzureAppConfiguration()
            .AddFeatureManagement();
    }

    /// <summary>
    /// Uses the Azure App Configuration feature.
    /// </summary>
    /// <param name="app">The app.</param>
    /// <returns>App builder.</returns>
    public static IApplicationBuilder UseAppConfigFeature(this WebApplication app)
    {
        var appConfigConnection = app.Configuration.GetConnectionString("AppConfig");
        return !string.IsNullOrEmpty(appConfigConnection)
            ? app.UseAzureAppConfiguration()
            : app;
    }
}
