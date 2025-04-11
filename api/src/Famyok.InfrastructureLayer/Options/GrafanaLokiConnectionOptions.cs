using Famyok.InfrastructureLayer.Exceptions;

namespace Famyok.InfrastructureLayer.Options;

public class GrafanaLokiConnectionOptions : BaseConnectionOptions
{
    protected override string EnvVarName => "GRAFANA_LOKI_URL";
}