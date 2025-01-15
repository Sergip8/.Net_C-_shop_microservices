namespace microStore.Services.ProductApi.Models.DTO
{
    public class CommentHeaderDTO
    {
        public int CommentHeaderId { get; set; }
        public string OverallScore { get; set; }
        public string QtyForStar { get; set; }
        public int CommentCount { get; set; }
        public int ProductId { get; set; }
        public int[] ScoreList { get; set; }
        public IEnumerable<CommentDTO> Comments { get; set; }

    }
}
