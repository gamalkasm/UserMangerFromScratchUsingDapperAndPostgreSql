using AuthenticationUsingDapperFromScratch.Domain.Interfaces;
using System.Data.Common;
using System.Data;
using AuthenticationUsingDapperFromScratch.Domain.Entities;
using Dapper;

namespace AuthenticationUsingDapperFromScratch.Infrastructure.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IDbConnection _connection;

        public CompanyRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            return await _connection.QueryAsync<Company>("SELECT * FROM Companies");
        }

        public async Task<Company> GetByIdAsync(int id)
        {
            return await _connection.QueryFirstOrDefaultAsync<Company>("SELECT * FROM Companies WHERE Id = @Id", new { Id = id });
        }

        public async Task<int> InsertAsync(Company company)
        {
            const string sql = "INSERT INTO Companies (Name) VALUES (@Name) RETURNING Id";
            return await _connection.ExecuteScalarAsync<int>(sql, company);
        }

        public async Task<bool> UpdateAsync(Company company)
        {
            const string sql = "UPDATE Companies SET Name = @Name WHERE Id = @Id";
            int rowsAffected = await _connection.ExecuteAsync(sql, company);
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            const string sql = "DELETE FROM Companies WHERE Id = @Id";
            int rowsAffected = await _connection.ExecuteAsync(sql, new { Id = id });
            return rowsAffected > 0;
        }
    }
}
