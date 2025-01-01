using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace microStore.Services.ProductApi.Models
{
    public class ProductImages : BaseEntity
    {
        public string ImageLabel { get; set; }
        public string ImageUrl { get; set; }
        public int ProductId { get; set; }
        [JsonIgnore]
        public Product Products { get; set; }


    }
}
