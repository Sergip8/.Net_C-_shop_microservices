namespace microStore.Services.InventoryApi.Models
{
    public class Inventory
    {
        public int InventoryId { get; set; }
        public int Quantity { get; set; }
        public decimal RetailPrice { get; set; }
        public int VendorId { get; set; }
        public int ProductId { get; set; }

    }
}
