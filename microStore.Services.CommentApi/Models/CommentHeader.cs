using System.ComponentModel.DataAnnotations.Schema;

namespace microStore.Services.CommentApi.Models
{
    public class CommentHeader
    {
        public int CommentHeaderId { get; set; }
        public float OverallScore { get; set; }
        public string QtyForStar { get; set; }
        public int CommentCount { get; set; }
        public int ProductId { get; set; }
        [NotMapped]
        public int[]? ScoreList { get; set; }
        public IEnumerable<Comment> Comments { get; set; }

    }
}
