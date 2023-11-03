using System.Data;
using Dapper;
using Npgsql;

namespace MyAPI.Data
{
  class DataContextDapper
  {
    IDbConnection _dbConnection;
    public DataContextDapper(IConfiguration config)
    {
      this._dbConnection = new NpgsqlConnection(config.GetConnectionString("DefaultConnection"));
    }

    public IEnumerable<T> LoadData<T>(string sql)
    {
      return this._dbConnection.Query<T>(sql);
    }

    public T LoadDataSingle<T>(string sql)
    {
      return this._dbConnection.QuerySingle<T>(sql);
    }

    public bool ExecuteSql(string sql)
    {
      return this._dbConnection.Execute(sql) > 0;
    }

    public int ExecuteSqlWithRowCount(string sql)
    {
      return this._dbConnection.Execute(sql);
    }
  }
}