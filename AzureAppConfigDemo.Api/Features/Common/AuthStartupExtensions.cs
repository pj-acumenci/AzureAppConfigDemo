namespace AzureAppConfigDemo.Api.Features.Common;

/// <summary>
/// Extensions for configuring auth at startup.
/// </summary>
public static class AuthStartupExtensions
{
    /// <summary>
    /// Adds the auth feature.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddAuthFeature(
        this IServiceCollection services) => services;

    /// <summary>
    /// Uses the auth feature.
    /// </summary>
    /// <param name="app">The app builder.</param>
    /// <returns>The same app builder.</returns>
    public static IApplicationBuilder UseAuthFeature(
        this WebApplication app) => app
            .UseAuthentication()
            .UseAuthorization();
}
