namespace microStore.Services.ProductApi.Models.DTO
{
    public class ProductUpdateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public decimal Current_price { get; set; }
        public decimal Previous_price { get; set; }
        public bool IsAvailable { get; set; } = true;
        public int BrandId { get; set; }
        public InventoryDTORequest Inventory { get; set; }
        public List<int> CategoryIds { get; set; }
        public List<ProductImages> ImagesToDelete { get; set; }
        public List<int> PropertyIds { get; set; }
    }
}
