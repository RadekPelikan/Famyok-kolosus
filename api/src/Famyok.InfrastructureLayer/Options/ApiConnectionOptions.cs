namespace Famyok.InfrastructureLayer.Options;

public class ApiConnectionOptions : BaseConnectionOptions
{
    protected override string EnvVarName => "API_URL";
}