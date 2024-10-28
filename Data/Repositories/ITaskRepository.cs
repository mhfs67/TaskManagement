using TaskManagement.API.Models;

namespace TaskManagement.API.Data.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Models.Task>> GetProjectTasks(int projectId);
        Task<Models.Task> GetTaskById(int taskId);
        Task<int> GetProjectTaskCount(int projectId);
        Task<int> GetPendingTasksCount(int projectId);
        Task<IEnumerable<Models.Task>> GetCompletedTasksByUserInPeriod(DateTime startDate);
        System.Threading.Tasks.Task AddTask(Models.Task task);
        System.Threading.Tasks.Task UpdateTask(System.Threading.Tasks.Task task);
        System.Threading.Tasks.Task DeleteTask(System.Threading.Tasks.Task task);
        System.Threading.Tasks.Task AddComment(Comment comment);
        System.Threading.Tasks.Task AddTaskHistory(TaskHistory history);
    }
}
