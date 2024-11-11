using System.ComponentModel.DataAnnotations;

namespace microStore.Services.ProductApi.Models
{
    public class ProductImages
    {
        [Key]
        public int ImageId { get; set; }
        public string ImageLabel { get; set; }
        public string ImageUrl { get; set; }
        public int ProductId { get; set; }


    }
}
