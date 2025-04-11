    using Famyok.InfrastructureLayer.Exceptions;

namespace Famyok.InfrastructureLayer.Options;

public class DbConnectionOptions
{
    public string Server { get; set; }
    public int Port { get; set; }
    public string Database { get; set; }
    public string User { get; set; }
    public string Password { get; set; }


    public string ConnectionString
    {
        set
        {
            _connectionString = value;
            if (string.IsNullOrWhiteSpace(_connectionString) &&
                string.IsNullOrWhiteSpace(Server) is false &&
                Port != 0 &&
                string.IsNullOrWhiteSpace(Database) is false &&
                string.IsNullOrWhiteSpace(User) is false &&
                string.IsNullOrWhiteSpace(Password) is false
               )
            {
                _connectionString = BuildConnectionString(Server, Port, Database, User, Password);
            }
            else if (string.IsNullOrWhiteSpace(_connectionString))
            {
                _connectionString =
                    BuildConnectionString(
                        GetEnvironmentVariableOrThrow("DB_SERVER"),
                        GetEnvironmentVariableOrThrow("DB_PORT"),
                        GetEnvironmentVariableOrThrow("DB_NAME"),
                        GetEnvironmentVariableOrThrow("DB_USER"),
                        GetEnvironmentVariableOrThrow("DB_PASSWORD")
                    );
            }
        }
        get => _connectionString ??
               throw new ConfigurationNotDefinedException($"DbConnectionOptions is missing required configuration.");
    }

    private string? _connectionString { get; set; }

    private string BuildConnectionString(string server, int port, string database, string user, string password)
        => BuildConnectionString(server, port.ToString(), database, user, password);

    private string BuildConnectionString(string server, string port, string database, string user, string password)
        => $"server={server};port={port};database={database};user={user};password={password}";

    private string GetEnvironmentVariableOrThrow(string environmentVariable) =>
        Environment.GetEnvironmentVariable(environmentVariable) ??
        throw new ConfigurationNotDefinedException($"{environmentVariable} environment variable is missing");
}