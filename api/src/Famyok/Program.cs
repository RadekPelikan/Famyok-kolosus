using Famyok.InfrastructureLayer.Extensions;
using Famyok.InfrastructureLayer.Options;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Formatting.Json;
using Serilog.Sinks.Grafana.Loki;


try
{
    var builder = WebApplication.CreateBuilder(args);

    builder
        .AddConfigurations()
        .AddSerilog();

    Log.Information("Starting up");

    builder
        .AddCorsAllowFamyok();

    builder.Services
        .AddControllers();

    builder.Services.AddSwaggerDoc("Famyok API", "v1");

    var app = builder.Build();

    var connectionOptions = app.Services.GetRequiredService<IOptions<ConnectionOptions>>().Value;

    app.UseCors("AllowFamyokOrigins");

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();

        app.UseSwaggerUI(options =>
        {
            options
                .AddSwaggerEndpoint(connectionOptions.Api, "Famyok API v1")
                .AddSwaggerEndpoint(connectionOptions.Identity, "Identity API v1");
        });
    }

    app.UseHttpsRedirection();

    // Add Serilog Request Logging Middleware
    app.UseSerilogRequestLogging(options =>
    {
        options.MessageTemplate = "Handled {RequestMethod} {RequestPath} responded with {StatusCode}";

        options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
        {
            // Enrich with custom labels
            diagnosticContext.Set("RequestHost", httpContext.Request.Host.ToString());
            diagnosticContext.Set("ResponseStatusCode", httpContext.Response.StatusCode);

            // You can add custom labels to differentiate request types, e.g., "log_type" for categorizing
            diagnosticContext.Set("log_type", "request");

            // Optionally, categorize as "error" for certain conditions
            if (httpContext.Response.StatusCode >= 400)
            {
                diagnosticContext.Set("log_type", "error");
            }
        };
    });

    app.UseAuthorization();

    app.MapControllers();

    app.Run(connectionOptions.Api.Url);
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}