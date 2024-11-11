using System.Text.Json.Serialization;

namespace microStore.Services.ProductApi.Models
{
    public class PropertyValue
    {
        public int PropertyValueId { get; set; }
        public string PropertyValueName { get; set; }
        public int PropertyId { get; set; }
        [JsonIgnore]
        public IEnumerable<Product> Products { get; set; }
    }
}
