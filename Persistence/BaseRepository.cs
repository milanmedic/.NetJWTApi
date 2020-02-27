using JwtApi.Persistence;
using System.Linq;

namespace JwtApi.Persistence {
    public abstract class BaseRepository {
        protected readonly AppDbContext _context;

        public BaseRepository(AppDbContext context) => _context = context; //The BaseRepository receives an instance of our AppDbContext through dependency injection and exposes a protected property (a property that can only be accessible by the children classes) called _context, that gives access to all methods we need to handle database operations.
    }
}