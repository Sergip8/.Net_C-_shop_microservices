using System.Text.Json.Serialization;

namespace microStore.Services.ProductApi.Models
{
    public class Brand : BaseEntity
    {
        public string BrandName { get; set; }

        [JsonIgnore]
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
