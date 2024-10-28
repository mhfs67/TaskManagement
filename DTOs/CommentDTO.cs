namespace TaskManagement.API.DTOs
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateCommentDTO
    {
        public string Content { get; set; }
    }
}
