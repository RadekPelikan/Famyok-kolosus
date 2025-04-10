using Famyok.InfrastructureLayer.Constants;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFamyokOrigins", policy =>
    {
        policy.WithOrigins(DevelopmentConstants.FAMYOK_URLS);
    });
});

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo() { Title = "Famyok Identity provider", Version = "v1" });
});

var app = builder.Build();

app.UseCors("AllowFamyokOrigins");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run(DevelopmentConstants.IDENTITY_URL);