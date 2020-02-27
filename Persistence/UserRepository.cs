using System.Collections.Generic;
using System.Threading.Tasks;
using JwtApi.Domain.Repositories;
using JwtApi.Domain.Models;
using JwtApi.Persistence;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace JwtApi.Persistence {
    public class UserRepository : BaseRepository, IUserRepository {

        public UserRepository(AppDbContext context) : base(context) {
            
        }
        public async Task CreateUser(User user){
            await _context.Users.AddAsync(user);
            _context.SaveChanges();
        }
        public async Task<User> FindUser(string email) {
            var existingUser = await _context.Users.FirstOrDefaultAsync(suser => suser.Email == email);
            return existingUser;
        }

        public async Task<User> GetById(int id){
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetByEmail(string email){
            return await _context.Users.FirstOrDefaultAsync(user => user.Email.Equals(email));
        }
    }
}