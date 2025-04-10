using Famyok.InfrastructureLayer.Constants;
using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Famyok.InfrastructureLayer.Extensions;

public static class SwaggerExtensions
{
    public static SwaggerUIOptions AddApi(this SwaggerUIOptions o)
    {
        o.SwaggerEndpoint($"{DevelopmentConstants.API_URL}/swagger/v1/swagger.json", "Famyok API v1");
        return o;
    }
    
    public static SwaggerUIOptions AddIdentity(this SwaggerUIOptions o)
    {
        o.SwaggerEndpoint($"{DevelopmentConstants.IDENTITY_URL}/swagger/v1/swagger.json", "Identity API v1");
        return o;
    }
}