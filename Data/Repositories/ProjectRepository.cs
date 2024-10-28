using TaskManagement.API.Models;
using Microsoft.EntityFrameworkCore;

namespace TaskManagement.API.Data.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Project>> GetUserProjects(int userId)
        {
            return await _context.Projects
                .Include(p => p.Tasks)
                .Where(p => p.UserId == userId)
                .ToListAsync();
        }

        public async Task<Project> GetProjectById(int projectId)
        {
            return await _context.Projects
                .Include(p => p.Tasks)
                .FirstOrDefaultAsync(p => p.Id == projectId);
        }

        public async Task AddProject(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProject(Project project)
        {
            _context.Entry(project).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProject(Project project)
        {
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }
    }
}
