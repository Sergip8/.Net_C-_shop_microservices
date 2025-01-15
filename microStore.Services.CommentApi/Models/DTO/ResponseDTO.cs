namespace microStore.Services.CommentApi.Models.DTO
{
    public class ResponseDTO
    {
        public object? Data { get; set; }
        public int Count { get; set; } = 0;
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;

    }
}
