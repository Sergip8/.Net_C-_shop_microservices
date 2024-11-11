

using microStore.Services.ProductApi.Models.DTO;

namespace microStore.Services.ProductApi.Service.IService
{
    public interface IInventoryService
    {
        Task<InventoryDTO> GetInventory(int productId);
    }
}
