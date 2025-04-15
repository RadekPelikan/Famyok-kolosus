using Famyok.InfrastructureLayer.Extensions;
using Famyok.InfrastructureLayer.Options;
using Microsoft.Extensions.Options;
using Serilog;


try
{
    var builder = WebApplication.CreateBuilder(args);

    builder
        .AddConfigurations()
        .AddCorsAllowFamyok()
        .AddSerilog();
    // Add services to the container.

    builder.Services
        .AddControllers();
    
    builder.Services.AddSwaggerDoc("Famyok Identity provider", "v1");

    var app = builder.Build();

    var connectionOptions = app.Services.GetRequiredService<IOptions<ConnectionOptions>>().Value;

    app.UseCors("AllowFamyokOrigins");

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    Log.Information($"Running on {connectionOptions.Identity.Url}");
    app.Run(connectionOptions.Identity.Url);
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