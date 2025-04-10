namespace Famyok.InfrastructureLayer.Exceptions;

public class ConfigurationNotDefinedException : Exception
{
    public ConfigurationNotDefinedException(string message) : base(message)
    {
    }
}