using Famyok.InfrastructureLayer.Extensions;
using Famyok.InfrastructureLayer.Options;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder
    .AddConfigurations()
    .AddCorsAllowFamyok();
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

app.Run(connectionOptions.Identity.Url);