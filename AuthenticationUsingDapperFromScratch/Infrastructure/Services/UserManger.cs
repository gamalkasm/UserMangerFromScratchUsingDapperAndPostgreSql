using AuthenticationUsingDapperFromScratch.Domain.Entities;
using AuthenticationUsingDapperFromScratch.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace AuthenticationUsingDapperFromScratch.Infrastructure.Services
{
    public class UserManger : IUserManger
    {

        private readonly IUserRepository _userRepository;
        public UserManger(IUserRepository userRepository, IDbTransaction transaction)
        {
            _userRepository = userRepository;
        }

        public async Task<IdentityResult> CreateAsync(User user, string password)
        {
            string hashedPassword = HashPassword(password);

            user.PasswordHash = hashedPassword;

            await _userRepository.CreateAsync(user);

            return IdentityResult.Success;
        }

        public async Task<User> FindByIdAsync(string userId)
        {
            return await _userRepository.FindByIdAsync(userId);
        }

        public async Task<User> FindByNameAsync(string userName)
        {
            return await _userRepository.FindByNameAsync(userName);
        }
        public async Task<IEnumerable<User>> FindByEmailAsync(string email)
        {
            return await _userRepository.FindByEmailAsync(email);
        }
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }
        public async Task<IdentityResult> UpdateAsync(User user)
        {
            // Validate user data if necessary
            await _userRepository.UpdateAsync(user);

            return IdentityResult.Success;
        }
        public async Task<IdentityResult> DeleteAsync(string userId)
        {
            await _userRepository.DeleteAsync(userId);

            return IdentityResult.Success;
        }

        public async Task<bool> CheckPasswordAsync(User user, string password)
        {
            var paswordHash = HashPassword(password);
            return paswordHash == user.PasswordHash;
        }

        private string HashPassword(string password)
        {
            // Implement password hashing logic (e.g., using BCrypt)
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
