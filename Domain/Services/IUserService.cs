using System.Collections.Generic;
using JwtApi.Domain.Models;
using System.Threading.Tasks;

namespace JwtApi.Domain.Services {
    public interface IUserService {
        Task<User> Authenticate(string email, string password);
        Task<User> RegisterUserAsync(User user, string password);
        Task<User> GetUserAsync(int id);
        Task<User> GetUserByEmailAsync(string email);
    }
}