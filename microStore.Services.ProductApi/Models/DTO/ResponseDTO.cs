namespace microStore.Services.ProductApi.Models.DTO
{
    public class ResponseDTO
    {
        public object? Data { get; set; }
        public int Count { get; set; } = 0;
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;

    }
    public class ResponseResultsDTO
    {
        public object? Products { get; set; }
        public object? Categories { get; set; }
        public object? Brands { get; set; }
        public object? Properties { get; set; }
        public int Count { get; set; } = 0;
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;

    }
}
