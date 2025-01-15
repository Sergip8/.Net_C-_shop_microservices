using System.ComponentModel.DataAnnotations;

namespace microStore.Services.ProductApi.Models.DTO
{
    public class ProductDTO
    {
        public BrandProductDTO Brand { get; set; }
        public List<CategoryProductDTO> Categories { get; set; }
        public List<ImageProductDTO> Images { get; set; }
        public ProductBasicDTO Product { get; set; }
    }
    public class ProductListDTO
    {
        public List<BrandDTO> Brands { get; set; }
        public List<CategoryDTO> Categories { get; set; }
        public List<ProductBasicDTO> Products { get; set; }
        public List<PropertiesFilter> Properties { get; set; }
        public ICollection<ImageProductDTO> Images { get; set; }
        public int Count { get; set; }
    }
    public class ProductBasicDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public decimal Current_price { get; set; }
        public decimal Previous_price { get; set; }
        public BrandDTO Brand { get; set; }

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
    public class PriceProductDTO
    {
        public int highPrice { get; set; }
        public string lowPrice { get; set; }
    }
    public class ImageProductDTO
    {
        public int Id { get; set; }
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

    public class PropertiesFilter
    {
       
            public int Id { get; set; }
            public string PropertyName { get; set; }
            public string PropertyValueName { get; set; }
       

    }
}
