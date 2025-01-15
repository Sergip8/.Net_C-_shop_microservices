using EventBusMessages.Events.Contracts;

namespace microStore.Services.CommentApi.Models.DTO
{
    public class CommentDTO
    {
        public int CommentId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Score { get; set; }
        public int Votes { get; set; }
        public int CommentHeaderId { get; set; }
        public GetUserDetailsResponse UserDetails { get; set; }


    }
    public class CommentWriteDTO
    {
        public int CommentId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Score { get; set; }
        public int Votes { get; set; }
        public int CommentHeaderId { get; set; }
        public string CommentUserId  { get; set; }


    }
    public class UserDetails
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }


}
