namespace microStore.Services.ProductApi.Models.DTO
{
    public class PaginationDTO
    {
        public int Page { get; set; }
        public int TotalCount { get; set; } = 0;
        public int size { get; set; }

    }
}
