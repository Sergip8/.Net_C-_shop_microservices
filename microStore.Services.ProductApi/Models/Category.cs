using System.Text.Json.Serialization;

namespace microStore.Services.ProductApi.Models
{
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; } = "";
        public int CategoryLevel { get; set; }
        public int CategoryParentId { get; set; }

        [JsonIgnore]
        public ICollection<Product> Products { get; set; } = new List<Product>();

    }
}
