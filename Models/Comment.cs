namespace TaskManagement.API.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public virtual Task Task { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
