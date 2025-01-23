namespace microStore.Services.ProductApi.Models.DTO
{
    public class BrandDTO
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
    }

    public class BrandCountDTO
    {
        public int Count { get; set; }
        public BrandDTO Brand { get; set; }
    }
}
