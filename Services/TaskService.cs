using TaskManagement.API.DTOs;
using TaskManagement.API.Models.Enums;
using TaskManagement.API.Models;

namespace TaskManagement.API.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepository;
        private const int MAX_TASKS_PER_PROJECT = 20;

        public TaskService(ITaskRepository taskRepository, IProjectRepository projectRepository)
        {
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
        }

        public async Task<IEnumerable<TaskDTO>> GetProjectTasks(int projectId)
        {
            var tasks = await _taskRepository.GetProjectTasks(projectId);
            return tasks.Select(MapToTaskDTO);
        }

        public async Task<TaskDTO> GetTaskById(int taskId)
        {
            var task = await _taskRepository.GetTaskById(taskId);
            if (task == null)
                throw new NotFoundException("Task not found");

            return MapToTaskDTO(task);
        }

        public async Task<TaskDTO> CreateTask(int projectId, int userId, CreateTaskDTO taskDto)
        {
            var taskCount = await _taskRepository.GetProjectTaskCount(projectId);
            if (taskCount >= MAX_TASKS_PER_PROJECT)
                throw new BusinessException($"Project has reached the maximum limit of {MAX_TASKS_PER_PROJECT} tasks");

            var task = new Task
            {
                Title = taskDto.Title,
                Description = taskDto.Description,
                DueDate = taskDto.DueDate,
                Status = TaskStatus.Pending,
                Priority = Enum.Parse<TaskPriority>(taskDto.Priority),
                ProjectId = projectId,
                CreatedAt = DateTime.UtcNow
            };

            await _taskRepository.AddTask(task);
            await AddTaskHistory(task.Id, userId, "Task created");

            return MapToTaskDTO(task);
        }

        public async Task<TaskDTO> UpdateTask(int taskId, int userId, UpdateTaskDTO taskDto)
        {
            var task = await _taskRepository.GetTaskById(taskId);
            if (task == null)
                throw new NotFoundException("Task not found");

            var changes = new List<string>();

            if (taskDto.Title != null && task.Title != taskDto.Title)
            {
                changes.Add($"Title changed from '{task.Title}' to '{taskDto.Title}'");
                task.Title = taskDto.Title;
            }

            if (taskDto.Description != null && task.Description != taskDto.Description)
            {
                changes.Add("Description updated");
                task.Description = taskDto.Description;
            }

            if (taskDto.DueDate.HasValue && task.DueDate != taskDto.DueDate.Value)
            {
                changes.Add($"Due date changed from '{task.DueDate}' to '{taskDto.DueDate}'");
                task.DueDate = taskDto.DueDate.Value;
            }

            if (taskDto.Status != null)
            {
                var newStatus = Enum.Parse<TaskStatus>(taskDto.Status);
                if (task.Status != newStatus)
                {
                    changes.Add($"Status changed from '{task.Status}' to '{newStatus}'");
                    task.Status = newStatus;
                }
            }

            if (changes.Any())
            {
                task.UpdatedAt = DateTime.UtcNow;
                await _taskRepository.UpdateTask(task);
                await AddTaskHistory(task.Id, userId, string.Join("; ", changes));
            }

            return MapToTaskDTO(task);
        }

        public async Task DeleteTask(int taskId, int userId)
        {
            var task = await _taskRepository.GetTaskById(taskId);
            if (task == null)
                throw new NotFoundException("Task not found");

            await _taskRepository.DeleteTask(task);
            await AddTaskHistory(task.Id, userId, "Task deleted");
        }

        public async Task AddComment(int taskId, int userId, CreateCommentDTO commentDto)
        {
            var task = await _taskRepository.GetTaskById(taskId);
            if (task == null)
                throw new NotFoundException("Task not found");

            var comment = new Comment
            {
                TaskId = taskId,
                UserId = userId,
                Content = commentDto.Content,
                CreatedAt = DateTime.UtcNow
            };

            await _taskRepository.AddComment(comment);
            await AddTaskHistory(taskId, userId, $"Comment added: {commentDto.Content}");
        }

        private async Task AddTaskHistory(int taskId, int userId, string description)
        {
            var history = new TaskHistory
            {
                TaskId = taskId,
                UserId = userId,
                ChangeDescription = description,
                ChangedAt = DateTime.UtcNow
            };

            await _taskRepository.AddTaskHistory(history);
        }

        private TaskDTO MapToTaskDTO(Task task)
        {
            return new TaskDTO
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                Status = task.Status.ToString(),
                Priority = task.Priority.ToString(),
                Comments = task.Comments?.Select(c => new CommentDTO
                {
                    Id = c.Id,
                    Content = c.Content,
                    UserName = c.User.Name,
                    CreatedAt = c.CreatedAt
                }).ToList()
            };
        }
    }
}
