namespace microStore.Services.ProductApi.Models.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int CategoryLevel { get; set; }
        public int CategoryParentId { get; set; }
        public IEnumerable<int> ProductIds { get; set; }
    }
}
