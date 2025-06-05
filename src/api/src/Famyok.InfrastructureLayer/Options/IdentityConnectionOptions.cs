namespace Famyok.InfrastructureLayer.Options;

public class IdentityConnectionOptions : BaseConnectionOptions
{
    protected override string EnvVarName => "IDENTITY_URL";
}