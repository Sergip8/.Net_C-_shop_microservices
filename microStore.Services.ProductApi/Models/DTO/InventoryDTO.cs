namespace microStore.Services.ProductApi.Models.DTO
{
    public class InventoryDTO
    {
        public int InventoryId { get; set; }
        public int Quantity { get; set; }
        public string VendorName { get; set; }
    }
    public class InventoryDTORequest
    {
        public int Quantity { get; set; }
        public decimal RetailPrice { get; set; }
        public int VendorId { get; set; }

    }
}
