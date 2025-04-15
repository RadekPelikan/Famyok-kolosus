using Famyok.InfrastructureLayer.Extensions;
using Famyok.InfrastructureLayer.Options;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Formatting.Json;

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