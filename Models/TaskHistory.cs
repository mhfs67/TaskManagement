namespace TaskManagement.API.Models
{
    public class TaskHistory
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public virtual Task Task { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string ChangeDescription { get; set; }
        public DateTime ChangedAt { get; set; }
    }
}
