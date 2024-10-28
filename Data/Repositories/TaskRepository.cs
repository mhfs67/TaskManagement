using TaskManagement.API.Models;
using Microsoft.EntityFrameworkCore;

namespace TaskManagement.API.Data.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.Task>> GetProjectTasks(int projectId)
        {
            return await _context.Tasks
                .Include(t => t.Comments)
                    .ThenInclude(c => c.User)
                .Where(t => t.ProjectId == projectId)
                .OrderBy(t => t.DueDate)
                .ToListAsync();
        }

        public async Task<Models.Task> GetTaskById(int taskId)
        {
            return await _context.Tasks
                .Include(t => t.Comments)
                    .ThenInclude(c => c.User)
                .Include(t => t.History)
                .FirstOrDefaultAsync(t => t.Id == taskId);
        }

        public async Task<int> GetProjectTaskCount(int projectId)
        {
            return await _context.Tasks
                .CountAsync(t => t.ProjectId == projectId);
        }

        public async Task<int> GetPendingTasksCount(int projectId)
        {
            return await _context.Tasks
                .CountAsync(t => t.ProjectId == projectId &&
                               (t.Status == TaskStatus.Pending || t.Status == TaskStatus.InProgress));
        }

        public async Task<IEnumerable<Models.Task>> GetCompletedTasksByUserInPeriod(DateTime startDate)
        {
            return await _context.Tasks
                .Include(t => t.Project)
                .Where(t => t.Status == TaskStatus.Completed &&
                           t.UpdatedAt >= startDate)
                .ToListAsync();
        }

        public async System.Threading.Tasks.Task AddTask(Models.Task task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task UpdateTask(System.Threading.Tasks.Task task)
        {
            _context.Entry(task).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task DeleteTask(System.Threading.Tasks.Task task)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task AddComment(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task AddTaskHistory(TaskHistory history)
        {
            _context.TaskHistories.Add(history);
            await _context.SaveChangesAsync();
        }
    }
}
