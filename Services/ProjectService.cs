using TaskManagement.API.DTOs;
using TaskManagement.API.Models;

namespace TaskManagement.API.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ITaskRepository _taskRepository;

        public ProjectService(IProjectRepository projectRepository, ITaskRepository taskRepository)
        {
            _projectRepository = projectRepository;
            _taskRepository = taskRepository;
        }

        public async Task<IEnumerable<ProjectDTO>> GetUserProjects(int userId)
        {
            var projects = await _projectRepository.GetUserProjects(userId);
            return projects.Select(p => new ProjectDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                TaskCount = p.Tasks.Count,
                CreatedAt = p.CreatedAt
            });
        }

        public async Task<ProjectDTO> GetProjectById(int userId, int projectId)
        {
            var project = await _projectRepository.GetProjectById(projectId);
            if (project == null || project.UserId != userId)
                throw new NotFoundException("Project not found");

            return new ProjectDTO
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                TaskCount = project.Tasks.Count,
                CreatedAt = project.CreatedAt
            };
        }

        public async Task<ProjectDTO> CreateProject(int userId, CreateProjectDTO projectDto)
        {
            var project = new Project
            {
                Name = projectDto.Name,
                Description = projectDto.Description,
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            await _projectRepository.AddProject(project);
            return new ProjectDTO
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                TaskCount = 0,
                CreatedAt = project.CreatedAt
            };
        }

        public async Task DeleteProject(int userId, int projectId)
        {
            var project = await _projectRepository.GetProjectById(projectId);
            if (project == null || project.UserId != userId)
                throw new NotFoundException("Project not found");

            if (!await CanDeleteProject(projectId))
                throw new BusinessException("Cannot delete project with pending tasks");

            await _projectRepository.DeleteProject(project);
        }

        public async Task<bool> CanDeleteProject(int projectId)
        {
            var pendingTasks = await _taskRepository.GetPendingTasksCount(projectId);
            return pendingTasks == 0;
        }
    }
}
