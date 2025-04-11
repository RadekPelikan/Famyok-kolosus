using Famyok.InfrastructureLayer.Exceptions;

namespace Famyok.InfrastructureLayer.Options;

public abstract class BaseConnectionOptions
{
    public int Port { get; set; }
    public string? Server { get; set; }

    public string Url
    {
        set => _url = value;
        get
        {
            if (string.IsNullOrWhiteSpace(_url) is false)
            {
                return _url;
            }
            if (Port != 0 && string.IsNullOrWhiteSpace(Server) is false)
            {
                return $"http://{Server}:{Port}";
            }
            if (string.IsNullOrWhiteSpace(_url))
            {
                return Environment.GetEnvironmentVariable(EnvVarName);
            }

            throw new ConfigurationNotDefinedException(
                $"{EnvVarName} was not defined in appsettings nor in environment variables.");
        }
    }

    private string _url;
    protected abstract string EnvVarName { get; }
}