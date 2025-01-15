using System.ComponentModel.DataAnnotations;

namespace microStore.Services.ShoppingCartApi.Models.DTO
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
    }

    public record ProductIdsResponse
    {
        public List<int> ProductIds { get; set; }
    }
}
