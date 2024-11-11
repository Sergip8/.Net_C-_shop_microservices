using System.ComponentModel.DataAnnotations;

namespace microStore.Services.ProductApi.Models.DTO
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public List<ImageProductDTO> ImageUrls { get; set; }
        public int BrandId { get; set; }
        public List<int> CategoryIds { get; set; }
    }
    public record ProductIdsRequest
    {
        public List<int> ProductIds { get; set; }
    }
    public class CategoryProductDTO
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
    public class BrandProductDTO
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
    }
    public class ImageProductDTO
    {
        public string ImageLabel { get; set; }
        public string ImageUrl { get; set; }
    }
    public class ProductResponseDTO
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public BrandProductDTO Brand { get; set; }
        public IEnumerable<CategoryProductDTO> CategoryProduct { get; set; }
    }
}
