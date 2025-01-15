using EventBusMessages.Events.Contracts;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using microStore.Services.InventoryApi.Data;

namespace microStore.Services.InventoryApi.EventBusConsumer
{
    public class ValidateInventoryConsumer : IConsumer<ValidateInventoryRequest>
    {
        private readonly AppDbContext _dbContext;

        public ValidateInventoryConsumer(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<ValidateInventoryRequest> context)
        {
            var products = context.Message.Products;
            var arrayRes = new List<ValidateInventory>();
            foreach (var product in products)
            {
                var inventory = await _dbContext.Inventories.Where(i => i.ProductId == product.ProductId).FirstAsync();
                if (inventory != null)
                {
                    arrayRes.Add(new ValidateInventory
                    {
                        ProductId = product.ProductId,
                        IsValid = inventory.Quantity > product.Quantity
                    });
                }
            }

            await context.RespondAsync(new ValidateInventoryResponse { InventoryResponse = arrayRes });
        }
    }
}
