using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.DTOs;
using TaskManagement.API.Services;

namespace TaskManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("project/{projectId}")]
        public async Task<ActionResult<IEnumerable<TaskDTO>>> GetProjectTasks(int projectId)
        {
            var tasks = await _taskService.GetProjectTasks(projectId);
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDTO>> GetTask(int id)
        {
            var task = await _taskService.GetTaskById(id);
            return Ok(task);
        }

        [HttpPost("project/{projectId}")]
        public async Task<ActionResult<TaskDTO>> CreateTask(int projectId, [FromBody] CreateTaskDTO taskDto)
        {
            var userId = GetCurrentUserId();
            var task = await _taskService.CreateTask(projectId, userId, taskDto);
            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TaskDTO>> UpdateTask(int id, [FromBody] UpdateTaskDTO taskDto)
        {
            var userId = GetCurrentUserId();
            var task = await _taskService.UpdateTask(id, userId, taskDto);
            return Ok(task);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTask(int id)
        {
            var userId = GetCurrentUserId();
            await _taskService.DeleteTask(id, userId);
            return NoContent();
        }

        [HttpPost("{id}/comments")]
        public async Task<ActionResult> AddComment(int id, [FromBody] CreateCommentDTO commentDto)
        {
            var userId = GetCurrentUserId();
            await _taskService.AddComment(id, userId, commentDto);
            return Ok();
        }

        private int GetCurrentUserId()
        {
            // Implement user authentication logic
            return 1; // Temporary placeholder
        }
    }
}
