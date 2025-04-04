﻿using Microsoft.AspNetCore.Builder;
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
    /// Conditionally adds the ASP.NET Core Developer Exception Page middleware based on configuration.
    /// </summary>
    /// <param name="app">The application builder used to configure the request pipeline.</param>
    /// <param name="configuration">The application configuration used to retrieve the setting.</param>
    /// <remarks>
    /// Checks the <c>DeveloperExceptionPage</c> configuration key. If it is set to <c>true</c>, 
    /// the <see cref="Microsoft.AspNetCore.Builder.DeveloperExceptionPageExtensions.UseDeveloperExceptionPage"/> middleware is added.
    /// </remarks>
    public static void AddDeveloperExceptionPage(this IApplicationBuilder app, IConfiguration configuration)
    {
        var enabled = configuration.GetValue<bool>("DeveloperExceptionPage");

        if (enabled)
            app.UseDeveloperExceptionPage();
    }
}