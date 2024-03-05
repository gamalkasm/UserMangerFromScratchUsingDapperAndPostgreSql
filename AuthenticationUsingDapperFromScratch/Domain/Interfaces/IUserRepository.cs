using AuthenticationUsingDapperFromScratch.Domain.Entities;
using System.Data;

namespace AuthenticationUsingDapperFromScratch.Domain.Interfaces
{
    public interface IUserRepository
    {
        //if you to went te=tansaction from repos
        Task CreateAsync(User user/*, IDbTransaction connection =null*/);
        Task<User> FindByIdAsync(string userId);
        Task<User> FindByNameAsync(string username);
        Task<IEnumerable<User>> FindByEmailAsync(string email);
        Task<IEnumerable<User>> GetAllAsync();
        Task UpdateAsync(User user);
        Task DeleteAsync(string userId);
    }
}
