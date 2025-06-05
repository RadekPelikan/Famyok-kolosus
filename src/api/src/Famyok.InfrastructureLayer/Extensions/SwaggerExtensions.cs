using Famyok.InfrastructureLayer.Options;
using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Famyok.InfrastructureLayer.Extensions;

public static class SwaggerExtensions
{
    public static SwaggerUIOptions AddSwaggerEndpoint(this SwaggerUIOptions o, BaseConnectionOptions connectionOptions, string name)
    {
        o.SwaggerEndpoint($"{connectionOptions.Url}/swagger/v1/swagger.json", name);
        return o;
    }
}