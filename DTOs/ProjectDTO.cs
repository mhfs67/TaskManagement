namespace TaskManagement.API.DTOs
{
    public class ProjectDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TaskCount { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateProjectDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
