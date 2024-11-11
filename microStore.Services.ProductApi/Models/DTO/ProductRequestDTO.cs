using System.Collections;

namespace microStore.Services.ProductApi.Models.DTO
{
    public class ProductRequestDTO
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public string Query { get; set; }
        public IEnumerable<FilterDTO> Filters { get; set; }
        public int PageSize { get; set; }
        public Order Order { get; set; }

    }
    public class Order
    {
        public string Field { get; set; }
        public string Direction { get; set; }
    }

    public class Filter
    {
        public string BrandId { get; set; }
        public IEnumerable<int> CategoriesId { get; set; }
    }
}
