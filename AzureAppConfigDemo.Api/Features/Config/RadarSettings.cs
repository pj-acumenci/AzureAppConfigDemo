namespace AzureAppConfigDemo.Api.Features.Config;

/// <summary>
/// Radar settings.
/// </summary>
public class RadarSettings
{
    /// <summary>
    /// Gets or sets the palette settings.
    /// </summary>
    public PaletteSettings Palette { get; set; } = new();
}

/// <summary>
/// Palette settings.
/// </summary>
public class PaletteSettings
{
    /// <summary>
    /// Gets or sets the background colour.
    /// </summary>
    public string? Background { get; set; }

    /// <summary>
    /// Gets or sets the foreground colour.
    /// </summary>
    public string? Foreground { get; set; }
}
