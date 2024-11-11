using System.Text.Json.Serialization;

namespace microStore.Services.ProductApi.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = "";
        public int CategoryLevel { get; set; }
        public int CategoryParentId { get; set; }

        [JsonIgnore]
        public IEnumerable<Product> Products { get; set; } = Enumerable.Empty<Product>();

    }
}
