using Microsoft.Extensions.Options;
using WildOwls.Model;
using System.Data;
using System.Data.SqlClient;

namespace WildOwls.Repository.Providers
{
    public class SqlProvider : ISqlProvider
    {
        private IDbConnection _connection = null;

        private readonly IOptions<SqlConnectionSettings> _connectionString;

        public SqlProvider(IOptions<SqlConnectionSettings> connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection GetDbConnection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = new SqlConnection(_connectionString.Value.DbConnectionString);
                }
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }
                return _connection;
            }
        }

        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Close();
                _connection.Dispose();
                _connection = null;
            }
        }
    }
}
