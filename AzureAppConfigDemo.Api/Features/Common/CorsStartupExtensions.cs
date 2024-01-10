namespace AzureAppConfigDemo.Api.Features.Common;

/// <summary>
/// Extensions for configuring cross-origin resource sharing at startup.
/// </summary>
public static class CorsStartupExtensions
{
    /// <summary>
    /// Adds the CORS feature.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddCorsFeature(
        this IServiceCollection services)
    {
        return services.AddCors(o => o
            .AddDefaultPolicy(builder => builder
                .WithMethods("GET", "POST", "PUT", "PATCH", "DELETE")
                .AllowAnyHeader()
                .AllowAnyOrigin()
                .SetIsOriginAllowedToAllowWildcardSubdomains()));
    }

    /// <summary>
    /// Uses the CORS feature.
    /// </summary>
    /// <param name="app">The app builder.</param>
    /// <returns>The same app builder.</returns>
    public static IApplicationBuilder UseCorsFeature(
        this IApplicationBuilder app)
    {
        return app.UseCors();
    }
}
