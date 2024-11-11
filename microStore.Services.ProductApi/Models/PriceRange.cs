namespace microStore.Services.ProductApi.Models
{
    public class PriceRange
    {
        public int PriceRangeId { get; set; }
        public decimal HighPrice { get; set; }
        public decimal LowPrice { get; set; }
    }
}
