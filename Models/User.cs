using System.Collections.Generic;
using TaskManagement.API.Models.Enums;

namespace TaskManagement.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}
