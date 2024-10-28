using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Services;
using TaskManagement.API.DTOs;

namespace TaskManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDTO>>> GetProjects()
        {
            var userId = GetCurrentUserId(); // Implement user authentication
            var projects = await _projectService.GetUserProjects(userId);
            return Ok(projects);
        }

        [HttpPost]
        public async Task<ActionResult<ProjectDTO>> CreateProject([FromBody] CreateProjectDTO projectDto)
        {
            var userId = GetCurrentUserId();
            var project = await _projectService.CreateProject(userId, projectDto);
            return CreatedAtAction(nameof(GetProjects), new { id = project.Id }, project);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProject(int id)
        {
            var userId = GetCurrentUserId();
            await _projectService.DeleteProject(userId, id);
            return NoContent();
        }

        private int GetCurrentUserId()
        {
            // Implement user authentication logic
            return 1; // Temporary placeholder
        }
    }
}
