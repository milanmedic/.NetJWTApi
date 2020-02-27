using System.Collections.Generic;
using System.Threading.Tasks;
using JwtApi.Domain.Models;

namespace JwtApi.Domain.Repositories {
    public interface IUserRepository {
        Task CreateUser(User user);
        Task<User> FindUser(string email);
        Task<User> GetById(int id);
        Task<User> GetByEmail(string email);
    }
}