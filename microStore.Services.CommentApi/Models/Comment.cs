namespace microStore.Services.CommentApi.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int Score { get; set; }
        public int Votes { get; set; }
        public int CommentHeaderId { get; set; }
    }
}
