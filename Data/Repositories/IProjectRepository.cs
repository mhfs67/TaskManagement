using TaskManagement.API.Models;

namespace TaskManagement.API.Data.Repositories
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetUserProjects(int userId);
        Task<Project> GetProjectById(int projectId);
        Task AddProject(Project project);
        Task UpdateProject(Project project);
        Task DeleteProject(Project project);
    }
}
