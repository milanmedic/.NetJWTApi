using JwtApi.Domain.Repositories;
using JwtApi.Domain.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace JwtApi.Persistence {
    public class ProjectRepository : BaseRepository, IProjectRepository {
        public ProjectRepository(AppDbContext context) : base(context){}
        public async Task CreateProject(Project project) {
            await _context.AddAsync(project);
            _context.SaveChanges();
        }
        public async Task<Project> GetProjectsById(int id) {
            return await _context.Projects.FindAsync(id);
        }
        public async Task<bool> RemoveProject(int id) {
            return false;
        }
        public async Task<ICollection<Project>> GetProjectsWithCreatorId(int id){
            return await _context.Projects.Where(p => p.CreatorId == id).ToListAsync();
        }
    }
}