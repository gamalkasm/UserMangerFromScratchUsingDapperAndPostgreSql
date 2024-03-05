using System.Data;

namespace AuthenticationUsingDapperFromScratch.Domain.Interfaces
{
    public interface IDBConnectionFactory
    {
        IDbConnection CreateConnection();
        //IDbTransaction CreateTransaction(IDbConnection connection);
    }
}
