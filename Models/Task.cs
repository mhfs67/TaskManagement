using System.Xml.Linq;
using TaskManagement.API.Models.Enums;

namespace TaskManagement.API.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public Task_Status Status { get; set; }
        public TaskPriority Priority { get; set; }
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public virtual ICollection<TaskHistory> History { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
