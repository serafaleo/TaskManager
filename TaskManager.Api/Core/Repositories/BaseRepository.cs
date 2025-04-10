using System.Data;
using System.Reflection;
using System.Text;
using Dapper;
using TaskManager.Api.Core.Services.Interfaces;
using TaskManager.Domain.Core.Models;
using TaskManager.Domain.Core.Repositories.Interfaces;

namespace TaskManager.Api.Core.Repositories;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
{
    protected readonly IDbConnectionService _db;
    protected readonly string _tableName;
    protected readonly string _idColumnName;
    protected readonly string _idParamName;

    public BaseRepository(IDbConnectionService db)
    {
        T tempModel;

        _db = db;
        _tableName = $"{typeof(T).Name}s";
        _idColumnName = nameof(tempModel.Id);
        _idParamName = BuildParameterName(nameof(tempModel.Id));
    }

    public async Task<List<T>> GetAllAsync()
    {
        string query = $"SELECT * FROM {_tableName}";

        using IDbConnection connection = _db.CreateConnection();
        return (await connection.QueryAsync<T>(query)).ToList();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        var queryParams = new { Id = id };

        string idParamName = BuildParameterName(nameof(queryParams.Id));

        string query = $"SELECT * FROM {_tableName} WHERE {_idColumnName} = {idParamName}";

        using IDbConnection connection = _db.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<T>(query, queryParams);
    }

    public async Task<int> CreateAsync(T model)
    {
        IEnumerable<PropertyInfo> properties = GetModelPropertiesExceptId(model);

        string columnNames = string.Join(", ", properties.Select(p => p.Name));
        string parameterNames = string.Join(", ", properties.Select(p => BuildParameterName(p.Name)));

        string query = $@"INSERT INTO {_tableName} ({columnNames})
                          OUTPUT INSERTED.{_idColumnName}
                          VALUES ({parameterNames})";

        using IDbConnection connection = _db.CreateConnection();
        return await connection.ExecuteScalarAsync<int>(query, model);
    }

    public async Task<bool> UpdateAsync(T model)
    {
        IEnumerable<PropertyInfo> properties = GetModelPropertiesExceptId(model);

        StringBuilder setClause = new StringBuilder();

        foreach (PropertyInfo property in properties)
        {
            setClause.Append($"{property.Name} = {BuildParameterName(property.Name)}, ");
        }

        setClause.Length -= 2; // Remove last comma

        string query = $@"UPDATE {_tableName}
                          SET {setClause}
                          WHERE {_idColumnName} = {_idParamName}";

        using IDbConnection connection = _db.CreateConnection();

        int rowsAffected = await connection.ExecuteAsync(query, model);
        return rowsAffected > 0;
    }

    public async Task<bool> DeleteByIdAsync(int id)
    {
        var queryParams = new { Id = id };

        string idParamName = BuildParameterName(nameof(queryParams.Id));

        string query = $"DELETE FROM {_tableName} WHERE {_idColumnName} = {idParamName}";

        IDbConnection connection = _db.CreateConnection();

        int rowsAffected = await connection.ExecuteAsync(query, queryParams);
        return rowsAffected > 0;
    }

    protected static string BuildParameterName(string fieldName)
    {
        return $"@{fieldName}";
    }

    private static IEnumerable<PropertyInfo> GetModelPropertiesExceptId(T model)
    {
        // NOTE(serafa.leo): Removing the Id property because we want the database to assign Id automatically.
        return typeof(T).GetProperties().Where(p => p.Name != nameof(model.Id));
    }
}
