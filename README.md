[![](https://img.shields.io/nuget/v/soenneker.extensions.applicationbuilder.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.applicationbuilder/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.applicationbuilder/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.applicationbuilder/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.extensions.applicationbuilder.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.applicationbuilder/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.applicationbuilder/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.applicationbuilder/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Extensions.ApplicationBuilder

A collection of helpful IApplicationBuilder extension methods.

## Installation

```bash
dotnet add package Soenneker.Extensions.ApplicationBuilder
```

## Quick start

```csharp
using Soenneker.Extensions.ApplicationBuilder;

// Given an existing IApplicationBuilder named app:
app.ConfigureHstsAndRedirection(configuration);
```

## Common operations

- `ConfigureHstsAndRedirection()` - Configures HTTP Strict Transport Security (HSTS) and HTTPS redirection for non-local, non-test environments.
- `UseAuthz()` - Adds authentication and authorization middleware to the request pipeline.
- `AddDeveloperExceptionPage()` - Conditionally adds the ASP.NET Core Developer Exception Page middleware based on configuration. Checks the `DeveloperExceptionPage` configuration key.
