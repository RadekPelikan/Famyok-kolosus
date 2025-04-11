using Famyok.InfrastructureLayer.Exceptions;
using Famyok.InfrastructureLayer.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Formatting.Json;
using Serilog.Sinks.Grafana.Loki;

namespace Famyok.InfrastructureLayer.Extensions;

public static class WebBuilderExtensions
{
    public static WebApplicationBuilder AddConfigurations(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;

        services.Configure<ConnectionOptions>(configuration.GetSection("Connection"));

        return builder;
    }

    public static WebApplicationBuilder AddSerilog(this WebApplicationBuilder builder)
    {
        
        builder.Host.UseSerilog((ctx, lc) =>
        {
            var grafanaLokiConnection = ctx.Configuration.GetSection("Connection:GrafanaLoki").Get<GrafanaLokiConnectionOptions>();

            if (grafanaLokiConnection is null)
            {
                throw new ConfigurationNotDefinedException("GrafanaLoki configuration is missing.");
            }
        
            lc
                .WriteTo.Console(
                    outputTemplate:
                    "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
                .WriteTo.GrafanaLoki(
                    "http://localhost:3100",
                    labels: new[]
                    {
                        new LokiLabel { Key = "app", Value = "famyok-api" },
                        new LokiLabel { Key = "env", Value = "dev" }
                    })
                .Enrich.FromLogContext()
                .ReadFrom.Configuration(ctx.Configuration);
        });
        return builder;
    }


    public static WebApplicationBuilder AddCorsAllowFamyok(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;

        var connectionOptions = configuration.GetSection("Connection").Get<ConnectionOptions>();

        services.AddCors(options =>
        {
            options.AddPolicy("AllowFamyokOrigins",
                policy => { policy.WithOrigins(connectionOptions.Api.Url, connectionOptions.Identity.Url); });
        });

        return builder;
    }
}