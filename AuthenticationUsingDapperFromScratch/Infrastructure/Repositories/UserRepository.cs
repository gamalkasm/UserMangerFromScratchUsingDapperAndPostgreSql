using AuthenticationUsingDapperFromScratch.Domain.Entities;
using AuthenticationUsingDapperFromScratch.Domain.Interfaces;
using Dapper;
using Npgsql;
using System.Data;
using System.Data.Common;
using System.Transactions;

namespace AuthenticationUsingDapperFromScratch.Infrastructure.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly IDbConnection _connection;

        public UserRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            string sql = "SELECT * FROM Users";
            return await _connection.QueryAsync<User>(sql);
        }
        //if you to went te=tansaction from repos
        public async Task<User> FindByIdAsync(string userId/*, IDbTransaction transaction = null*/)
        {
            string sql = "SELECT * FROM Users WHERE Id = @Id";
            return await _connection.QueryFirstOrDefaultAsync<User>(sql, new { Id = userId }/*, transaction*/);
        }

        public async Task<User> FindByNameAsync(string userName)
        {
            string sql = "SELECT * FROM Users WHERE UserName = @UserName";
            return await _connection.QueryFirstOrDefaultAsync<User>(sql, new { UserName = userName });
        }
        public async Task<IEnumerable<User>> FindByEmailAsync(string email)
        {
            string sql = "SELECT * FROM Users WHERE Email = @Email";
            return await _connection.QueryAsync<User>(sql, new { Email = email });
        }
        public async Task CreateAsync(User user)
        {
            string sql = "INSERT INTO Users (Id, UserName, PasswordHash) VALUES (@Id, @UserName, @PasswordHash)";
            await _connection.ExecuteAsync(sql, user);
        }
        public async Task UpdateAsync(User user)
        {
            string sql = "UPDATE Users SET UserName = @UserName, PasswordHash = @PasswordHash WHERE Id = @Id";
            await _connection.ExecuteAsync(sql, user);
        }

        public async Task DeleteAsync(string userId)
        {
            string sql = "DELETE FROM Users WHERE Id = @Id";
            await _connection.ExecuteAsync(sql, new { Id=userId });
        }
    }
}
