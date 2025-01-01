using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace microStore.Services.ProductApi.Models
{
    public class PropertyValue : BaseEntity
    {
        public string PropertyValueName { get; set; }
        [ForeignKey("PropertyId")]
        public int PropertyId { get; set; }
        public Property Property { get; set; }
        [JsonIgnore]
        public ICollection<Product> Products { get; set; }
    }
}
