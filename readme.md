# Azure App Configuration
## What is it?
- A place to easily store and manage settings for an unlimited number of disparate applications – in one place!
- Massively interoperable – as it is reachable over REST API and Azure SDK (as well as the following NuGet packages):
  - `Microsoft.Azure.AppConfiguration.AspNetCore`
  - `Microsoft.FeatureManagement.AspNetCore`
- First-class support for: Azure App Service, serverless/compute resources like Azure Functions and deployment pipelines
- .NET and Java packages, inc. ASP.NET / IConfiguration
- A fully-managed Azure service with up to 99.9% uptime SLA (Standard Tier)
- Security can be hardened via Azure managed identity

## What problem(s) does it solve?
### Config vs Code
- Application code should be identical across deployments (for a given version) [[1]](#refs)
- Anything that needs to change (or might need to one day) should be separated from the code base, lest we incur an unmanageable maintenance burden
- “Config” might include simple key-value pairs, as well as feature activation state
- Even with identical code bases, we can still use configuration to create profound behavioural differences:
  - E.g. feature flagging of entire modules, etc

### Operational Maintenance
- A software system may comprise of many independently-running applications
= A given feature (or key-value pair) may have relevance in multiple apps…
- …But this simple fact should not distort the solution design
- Application boundaries have other more important criteria (e.g. security, scalability, etc)
- We need a single place to be able to set system-wide configuration (…ideally at runtime).
- Must be performant and reliable; ideally hosted separately
  - Often, there may be configuration that spans multiple applications - it may not be appropriate for this to live in one of them
  - Plus there is likely to be lots of demand (albeit probably sporadic) to constantly deliver config values - this shouldnt be able to negatively impact on the application
  - Separate hosting is also operationally powerful, as it allows us to pull configuration "out-of-band" (e.g. during CI/CD pipeline) to avoid circular references

## So what's the solution?
Azure App Config is a tailor-made, hosted Azure resource.
Configuration state can be obtained several ways, (inc. Azure REST API) …But where it especially shines in my opinion, is in ASP.NET.

- Libraries which work with well-known entities such as IConfiguration and IOptionsSnapshot<T>
- Leads to a very light touch from the application perspective: we can generally continue to use the above entities as previous
- There is a whole caching mechanism (included with Azure App Config), some of whose behaviour is exposed on startup
- App Config includes the ability to tag entries, so an app can be configured to only look for certain tags, etc
- Feature management is catered for separately – both in terms of  the user experience in Azure, as well as library support
- The “Standard” tier includes a 99.9% uptime SLA [[2]](#refs)

## Can I see it in action?
Sure - that's why this repo exists :)
(All you need is access to an existing Azure App Configuration instance.)
Just bung your connection string in [appsettings.local.json](AzureAppConfigDemo.Api/appsettings.local.json#L3)

### Behaviour
Hopefully, the relative simplicity of the implementation comes across.

(There is a small amount of boilerplate (called from Program.cs) [here](AzureAppConfigDemo.Api/Features/Common/AppConfigStartupExtensions.cs#L24-L33)).

Without a connection string, whatever config provided by appsettings / prior mechanism continue to be available.

This is likely to be the "default" position for general development.

But to benefit from a similar experience as a deployed application, you can do the following:
  - Provide a valid connection string to an existing Azure App Config resource
  - Specify whatever set of keys (and feature flags) you wish to control dynamically in Azure App Config
  - Observe the dynamic behaviour, by calling the various endpoints (e.g. via Swagger UI)

It is also worth mentioning the `FeatureGate` attribute - which can be added to controllers, or specific endpoints.

With FeatureGate, if the specified feature is `disabled`, then requests to the affected endpoints will return 404s.
(NB: Swagger is not aware of this runtime check - hence will continue to list the endpoint, regardless of disabled feature state).


## <a name="refs"></a> References
1. https://12factor.net/config
1. https://azure.github.io/PSRule.Rules.Azure/en/rules/Azure.AppConfig.SKU/#description