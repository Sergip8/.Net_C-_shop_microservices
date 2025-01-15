using Google.Protobuf.Collections;
using InventoryServiceClient;
using microStore.Services.ProductApi.Models;
using microStore.Services.ProductApi.Models.DTO;

namespace microStore.Services.ProductApi.Contracts
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        Task<ProductAvailability> GetInventoryAvailability(int id);
        Task<ProductAvailabilityList> GetProductInventoryList(RepeatedField<int> productIds);
        Task CreateProduct(IFormCollection form);
        Task UpdateProduct(IFormCollection form);
    }
}
