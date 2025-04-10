using Famyok.InfrastructureLayer.Exceptions;

namespace Famyok.InfrastructureLayer.Constants;

public static class DevelopmentConstants
{
    public static int API_PORT = 3080;
    public static int IDENTITY_PORT = 3090;

    public static string API_URL => GetEnvironmentUrl("API_URL") ?? $"http://localhost:{API_PORT}";

    public static string IDENTITY_URL => GetEnvironmentUrl("IDENTITY_URL") ?? $"http://localhost:{IDENTITY_PORT}";

    public static string[] FAMYOK_URLS = new[] { API_URL, IDENTITY_URL };

    private static string? GetEnvironmentUrl(string envVarName)
    {
        var url = Environment.GetEnvironmentVariable(envVarName);
#if !DEBUG
            if (apiUrl is null)
            {
                throw new ConfigurationNotDefinedException($"{envSelector} not defined on production in environment variables");
            }
#endif
        return url;
    }
}