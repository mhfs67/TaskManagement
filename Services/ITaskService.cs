using TaskManagement.API.DTOs;

namespace TaskManagement.API.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDTO>> GetProjectTasks(int projectId);
        Task<TaskDTO> GetTaskById(int taskId);
        Task<TaskDTO> CreateTask(int projectId, int userId, CreateTaskDTO taskDto);
        Task<TaskDTO> UpdateTask(int taskId, int userId, UpdateTaskDTO taskDto);
        Task DeleteTask(int taskId, int userId);
        Task AddComment(int taskId, int userId, CreateCommentDTO commentDto);
    }
}
