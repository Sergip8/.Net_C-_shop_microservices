using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace microStore.Services.ProductApi.Models
{
    public class Property
    {
        [Key]
        public int PropertyId { get; set; }
        public bool IsPrincipal { get; set; }
        public string PropertyName { get; set; }
        public int PropertyTypeId { get; set; }
        public int PropertyOrder { get; set; }

        [JsonIgnore]
        public IEnumerable<PropertyValue> PropertyValues { get; set; }


    }
}
