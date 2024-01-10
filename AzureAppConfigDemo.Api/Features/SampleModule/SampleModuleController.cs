namespace AzureAppConfigDemo.Api.Features.Config;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;

/// <summary>
/// Config controller.
/// </summary>
[ApiController]
[AllowAnonymous]
[Route("[controller]")]
[FeatureGate(nameof(FeatureFlagOptions.SampleRadarModule))]
public class SampleModuleController
{
    /// <summary>
    /// Gets the current date and time.
    /// </summary>
    /// <returns>The current date and time, in UTC.</returns>
    [HttpGet]
    [Route("date")]
    public DateTimeOffset GetDate() => DateTimeOffset.UtcNow;

    /// <summary>
    /// Gets a random number.
    /// </summary>
    /// <returns>A random number.</returns>
    [HttpGet]
    [Route("random")]
    public int GetRandomNumber() => Random.Shared.Next(1, 1000);
}
