namespace microStore.Services.ProductApi.Models
{
    public class ProductPropertyValue
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int PropertyValueId { get; set; }
        public PropertyValue PropertyValue { get; set; }
    }
}
