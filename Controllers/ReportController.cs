using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Manager")]
    public class ReportController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        public ReportController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [HttpGet("performance")]
        public async Task<ActionResult<object>> GetPerformanceReport()
        {
            var thirtyDaysAgo = DateTime.UtcNow.AddDays(-30);
            var completedTasks = await _taskRepository.GetCompletedTasksByUserInPeriod(thirtyDaysAgo);

            var report = completedTasks
                .GroupBy(t => t.Project.UserId)
                .Select(g => new
                {
                    UserId = g.Key,
                    CompletedTasks = g.Count(),
                    AverageTasksPerDay = Math.Round((double)g.Count() / 30, 2)
                });

            return Ok(report);
        }
    }
}
