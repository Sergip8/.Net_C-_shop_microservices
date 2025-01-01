namespace microStore.Services.InventoryApi.Service
{

    using Grpc.Core;
    using InventoryService;
    using Microsoft.EntityFrameworkCore;
    using microStore.Services.InventoryApi.Data;
    using microStore.Services.InventoryApi.Models;
    using System;

    public class InventoryServices : InventoryProto.InventoryProtoBase
    {
        private readonly AppDbContext _dbContext;

        public InventoryServices(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<ProductAvailability> CheckAvailability(ProductRequest request, ServerCallContext context)
        {
            var inventory = await _dbContext.Inventories.Where(x => x.ProductId == request.ProductId)
                 .Join(
                    _dbContext.Vendors,
                    inventory => inventory.VendorId,
                    vendor => vendor.VendorId,
                    (inventory, vendor) => new
                    {
                        inventoryId = inventory.InventoryId,
                        quantity = inventory.Quantity,
                        vendorName = vendor.VendorName

                    }).FirstAsync();

            if (inventory == null)
            {
                return new ProductAvailability
                {
                    IsAvailable = false,
                    Stock = 0,
                    VendorName = ""
                };
            }

            return new ProductAvailability
            {
                IsAvailable = inventory.quantity > 0,
                Stock = inventory.quantity,
                VendorName = inventory.vendorName
            };
        }
        public override async Task<CreateInventoryResponse> CreateProductInventory(CreateInventoryRequest createInventory, ServerCallContext context)
        {
            var inventory = new Inventory
            {
                ProductId = createInventory.ProductId,
                Quantity = createInventory.Quantity,
                RetailPrice = Convert.ToDecimal(createInventory.RetailPrice),
                VendorId = createInventory.VendorId,

            };

            _dbContext.Inventories.Add(inventory);
            await _dbContext.SaveChangesAsync();
            Console.WriteLine(inventory.ProductId);
            Console.WriteLine(inventory.InventoryId);
            return new CreateInventoryResponse
            {
                InventoryId = inventory.InventoryId,
                Quantity = inventory.Quantity,
                VendorId = inventory.VendorId
            };
        }
    }
}
