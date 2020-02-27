using JwtApi.Domain.Models;
using JwtApi.Domain.Repositories;
using JwtApi.Domain.Services;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace JwtApi.Services {
    public class ProjectService : IProjectService {
        private readonly IProjectRepository _projectRepository;
        public ProjectService(IProjectRepository projectRepository) {
            _projectRepository = projectRepository;
        }
        public async Task<bool> AddNewProjectAsync(Project project) {
                await _projectRepository.CreateProject(project);
                return true;
        }
        public async Task<Project> GetProjectAsync(int id) {
            try {
                var project = await _projectRepository.GetProjectsById(id);
                return project;
            } catch(AppException ex){
                throw new AppException("Project doesn't exist!");
            }     
        }
        public async Task<ICollection<Project>> GetProjectsWithCreatorIdAsync(int id){
            return await _projectRepository.GetProjectsWithCreatorId(id);
        }
        public async Task<bool> DeleteProjectAsync(int id) {
            return false;
        }
    }
}