using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace microStore.Services.ProductApi.Models
{
    public class PropertyType
    {
        [Key]
        public int PropertyTypeId { get; set; }
        public string PropertyTypeName { get; set; }
        public string PropertyTypeDescription { get; set; }
        public int PropertyTypeOrder { get; set; }

        public IEnumerable<Property> Properties { get; set; }

    }
}
