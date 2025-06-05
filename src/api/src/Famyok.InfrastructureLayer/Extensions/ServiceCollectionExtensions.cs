using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Famyok.InfrastructureLayer.Extensions;

public static class ServiceCollectionExtensions
{

    public static IServiceCollection AddSwaggerDoc(this IServiceCollection services, string title, string version )
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(version, new OpenApiInfo() { Title = title , Version = version });
        });

        return services;
    }
}