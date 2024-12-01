using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace microStore.Services.ProductApi.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public decimal Current_price { get; set; }
        public decimal Previous_price { get; set; }
        public int BrandId { get; set; }
        [JsonIgnore]
        public IEnumerable<Category> Categories { get; set; }
        [JsonIgnore]
        public IEnumerable<ProductImages> Images { get; set; }
        [JsonIgnore]
        public IEnumerable<PropertyValue> Properties { get; set; }
    }

    public class ProductsCategories
    {
        public int CategoryId { get; set; }
        public int ProductId { get; set; }
    }
    public class ProductsImages
    {
        public int ImageId { get; set; }
        public int ProductId { get; set; }
    }
}
