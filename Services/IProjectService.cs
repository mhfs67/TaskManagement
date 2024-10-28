using TaskManagement.API.DTOs;

namespace TaskManagement.API.Services
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDTO>> GetUserProjects(int userId);
        Task<ProjectDTO> GetProjectById(int userId, int projectId);
        Task<ProjectDTO> CreateProject(int userId, CreateProjectDTO projectDto);
        Task DeleteProject(int userId, int projectId);
        Task<bool> CanDeleteProject(int projectId);
    }
}
