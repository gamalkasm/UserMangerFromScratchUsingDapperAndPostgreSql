using AuthenticationUsingDapperFromScratch.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace AuthenticationUsingDapperFromScratch.Infrastructure.Factories
{
    public class PostgresDBConnectionFactory : IDBConnectionFactory
    {
        private readonly IConfiguration configuration;

        public PostgresDBConnectionFactory(IConfiguration configuration) 
        {
            this.configuration = configuration;
        }
        public IDbConnection CreateConnection()
        {
            var connectionString = configuration.GetConnectionString("MyDatabaseConnection");
            return new NpgsqlConnection(connectionString);

        }

        //public IDbTransaction CreateTransaction(IDbConnection connection)
        //{
        //    if (connection == null)
        //    {
        //        throw new ArgumentNullException(nameof(connection));
        //    }

        //    if (connection.State != ConnectionState.Open)
        //    {
        //        connection.Open();
        //    }

        //    return connection.BeginTransaction();
        //}
    }
}
