using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Soenneker.Enums.DeployEnvironment;
using Soenneker.Extensions.Configuration;

namespace Soenneker.Extensions.ApplicationBuilder;

/// <summary>
/// A collection of helpful IApplicationBuilder extension methods
/// </summary>
public static class ApplicationBuilderExtension
{
    /// <summary>
    /// Configures HTTP Strict Transport Security (HSTS) and HTTPS redirection for non-local, non-test environments.
    /// </summary>
    /// <param name="app">The application builder used to configure the request pipeline.</param>
    /// <param name="configuration">The application configuration used to determine the deployment environment.</param>
    /// <remarks>
    /// HSTS and HTTPS redirection are only enabled if the environment is not Local or Test.
    /// For more information on HSTS, see: https://aka.ms/aspnetcore-hsts.
    /// </remarks>
    public static void ConfigureHstsAndRedirection(this IApplicationBuilder app, IConfiguration configuration)
    {
        DeployEnvironment deployEnvironment = DeployEnvironment.FromValue(configuration.GetValueStrict<string>("Environment"));

        if (deployEnvironment == DeployEnvironment.Local || deployEnvironment == DeployEnvironment.Test)
            return;

        app.UseHsts();
        app.UseHttpsRedirection();
    }

    /// <summary>
    /// Adds authentication and authorization middleware to the request pipeline.
    /// </summary>
    /// <param name="app">The application builder used to configure the request pipeline.</param>
    public static void UseAuthz(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }

    /// <summary>
    /// Adds the configured CORS policy to the request pipeline.
    /// </summary>
    /// <param name="app">The application builder used to configure the request pipeline.</param>
    public static void UseCorsPolicy(this IApplicationBuilder app)
    {
        app.UseCors();
    }
}