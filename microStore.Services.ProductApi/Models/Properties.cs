using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace microStore.Services.ProductApi.Models
{
    public class Property : BaseEntity
    {

        public bool IsPrincipal { get; set; } = false;
        public string PropertyName { get; set; }

        [ForeignKey("PropertyTypeId")]
        public int PropertyTypeId { get; set; }

        public virtual PropertyType PropertyType { get; set; }
        public int PropertyOrder { get; set; }

        [JsonIgnore]
        public virtual ICollection<PropertyValue> PropertyValues { get; set; }
    }
}
