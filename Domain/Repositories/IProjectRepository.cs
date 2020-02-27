using System.Threading.Tasks;
using JwtApi.Domain.Models;
using System.Collections.Generic;

namespace JwtApi.Domain.Repositories {
    public interface IProjectRepository {
        Task CreateProject(Project project);
        Task<Project> GetProjectsById(int id);
        Task<bool> RemoveProject(int id);
        Task<ICollection<Project>> GetProjectsWithCreatorId(int id);
    }
}