using System.Data;

namespace TaskManager.Api.Core.Services.Interfaces;

public interface IDbConnectionService
{
    public IDbConnection CreateConnection();
}
