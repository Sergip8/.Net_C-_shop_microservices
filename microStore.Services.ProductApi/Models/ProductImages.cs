using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace microStore.Services.ProductApi.Models
{
    public class ProductImages
    {
        [Key]
        public int ImageId { get; set; }
        public string ImageLabel { get; set; }
        public string ImageUrl { get; set; }

        [JsonIgnore]
        public IEnumerable<Product> Products { get; set; } = Enumerable.Empty<Product>();


    }
}
