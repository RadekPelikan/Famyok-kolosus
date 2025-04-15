namespace Famyok.InfrastructureLayer.Options;

public class ConnectionOptions
{
    public ApiConnectionOptions Api { get; set; }
    public IdentityConnectionOptions Identity { get; set; }
    public DbConnectionOptions Db { get; set; }
}