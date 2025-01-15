namespace microStore.Services.CommentApi.Models.DTO
{
    public class CommentHeaderDTO
    {
        public int CommentHeaderId { get; set; }
        public float OverallScore { get; set; }
        public string QtyForStar { get; set; }
        public int CommentCount { get; set; }
        public int ProductId { get; set; }
        public int[]? ScoreList { get; set; }
        public IEnumerable<CommentDTO> Comments { get; set; }

    }
    public class CommentHeaderWriteDTO
    {
        public int CommentHeaderId { get; set; }
        public float OverallScore { get; set; }
        public string QtyForStar { get; set; }
        public int CommentCount { get; set; }
        public int ProductId { get; set; }
        public int[]? ScoreList { get; set; }
        public IEnumerable<CommentWriteDTO> Comments { get; set; }

    }
}
