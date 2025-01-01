using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace microStore.Services.ProductApi.Models
{
    public class Product : BaseEntity
    {

        [Required]
        public string Name { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public decimal Current_price { get; set; }
        public decimal Previous_price { get; set; }
        public bool IsAvailable { get; set; } = true;
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        //[JsonIgnore]
        public ICollection<Category> Categories { get; set; } = new List<Category>();
        //[JsonIgnore]
        public ICollection<ProductImages> Images { get; set; } = [];
        //[JsonIgnore]
        public ICollection<PropertyValue> Properties { get; set; } = new List<PropertyValue>();
    }

    //public class ProductsCategories
    //{
    //    public int CategoryId { get; set; }
    //    public int ProductId { get; set; }
    //    public Product Product { get; set; }
    //    public Category Category { get; set; }
    //}
    //public class ProductsImages
    //{
    //    public int ImageId { get; set; }
    //    public int ProductId { get; set; }
    //}
}
