[![](https://img.shields.io/nuget/v/soenneker.extensions.applicationbuilder.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.applicationbuilder/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.applicationbuilder/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.applicationbuilder/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.extensions.applicationbuilder.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.applicationbuilder/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.applicationbuilder/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.applicationbuilder/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Extensions.ApplicationBuilder

Focused ASP.NET Core pipeline helpers for HTTPS/HSTS, authentication and authorization, and a safely gated developer exception page.

## Installation

```bash
dotnet add package Soenneker.Extensions.ApplicationBuilder
```

## Configuration

The environment-aware helpers read the package-specific `Environment` key. Its value must exactly match a `DeployEnvironment` value such as `Local`, `Test`, `Development`, `Staging`, or `Production`.

```json
{
  "Environment": "Local",
  "DeveloperExceptionPage": true
}
```

This key is independent of `ASPNETCORE_ENVIRONMENT` and `DOTNET_ENVIRONMENT`; map or populate it explicitly. A missing or unknown `Environment` value causes an environment-aware helper to fail rather than silently choosing a security posture.

## Pipeline usage

```csharp
using Soenneker.Extensions.ApplicationBuilder;

WebApplication app = builder.Build();

// Place exception handling early so it can observe downstream failures.
app.AddDeveloperExceptionPage(app.Configuration);
app.ConfigureHstsAndRedirection(app.Configuration);

app.UseRouting();
app.UseAuthz();

app.MapControllers();
app.Run();
```

`ConfigureHstsAndRedirection()` adds HSTS and HTTPS redirection in every defined environment except `Local` and `Test`. It configures middleware only: certificate binding, forwarded headers, proxy behavior, HSTS duration, and HTTPS ports still come from the host and ASP.NET Core configuration. Configure trusted proxy headers before redirection when TLS terminates upstream, or redirects can target the wrong scheme/port.

`UseAuthz()` calls `UseAuthentication()` and then `UseAuthorization()`. Register the corresponding services and schemes separately. Place it after routing and before endpoints that depend on authorization metadata. Calling the helper does not require authentication globally; endpoint policies and fallback policies determine access.

`AddDeveloperExceptionPage()` adds the developer exception page only when `DeveloperExceptionPage` is `true` and `Environment` is `Local` or `Development`. The flag is ignored in `Test`, `E2E`, `Staging`, and `Production` to prevent detailed exception data from being exposed. Configure a production-safe exception handler separately.
