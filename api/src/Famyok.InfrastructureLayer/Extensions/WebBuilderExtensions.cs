using Famyok.InfrastructureLayer.Exceptions;
using Famyok.InfrastructureLayer.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Formatting.Json;

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
            lc
                .WriteTo.Console(
                    outputTemplate:
                    "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")

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