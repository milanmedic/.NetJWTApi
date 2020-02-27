using System.Threading.Tasks;
using JwtApi.Domain.Models;
using System.Collections.Generic;

namespace JwtApi.Domain.Services {
    public interface IProjectService {
        Task<bool> AddNewProjectAsync(Project project);
        Task<Project> GetProjectAsync(int id);
        Task<bool> DeleteProjectAsync(int id);
        Task<ICollection<Project>> GetProjectsWithCreatorIdAsync(int id);
    }
}