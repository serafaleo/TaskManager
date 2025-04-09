using System.Data;
using Microsoft.Data.SqlClient;
using TaskManager.Api.Core.Services.Interfaces;

namespace TaskManager.Api.Core.Services;

public class DbConnectionService : IDbConnectionService
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public DbConnectionService(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("SQLServerConnectionString")!;
    }

    public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
}
