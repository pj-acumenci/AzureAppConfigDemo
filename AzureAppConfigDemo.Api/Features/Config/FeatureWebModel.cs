namespace AzureAppConfigDemo.Api.Features.Config;

/// <summary>
/// Feature flag information.
/// </summary>
/// <param name="Name">The name of the feature.</param>
/// <param name="Description">Supplementary information about the feature.</param>
/// <param name="Enabled">Whether the feature is currently enabled.</param>
public record FeatureWebModel(
    string Name,
    string? Description,
    bool Enabled);
