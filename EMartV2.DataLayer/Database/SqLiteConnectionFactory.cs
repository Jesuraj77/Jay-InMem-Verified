using Dapper;
using Microsoft.Data.Sqlite;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace EMartV2.DataLayer.Database
{
    public class SqLiteConnectionFactory : IDbConnectionFactory
    {
        private readonly string _dbLocation;

        public SqLiteConnectionFactory(string dbLocation)
        {
            _dbLocation = dbLocation;
        }

        public async Task<IDbConnection> CreateConnectionAsync()
        {
            var connection = new SqliteConnection($"Data Source={_dbLocation}");
            await connection.OpenAsync();
            return connection;
        }

        public async Task SetupAsync()
        {
            if (!File.Exists(_dbLocation))
            {
                File.Create(_dbLocation).Close();
                var connection = await CreateConnectionAsync();
                await connection.ExecuteAsync("CREATE TABLE Products (Id TEXT PRIMARY KEY NOT NULL, Name TEXT NULL);");
            }
        }
    }
}
