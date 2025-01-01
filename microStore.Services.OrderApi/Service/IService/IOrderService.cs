using microStore.Services.OrderApi.Models.DTO;

namespace microStore.Services.OrderApi.Service.IService
{
    public interface IOrderService
    {
        Task<ResponseDTO> CreateOrderAsync(CreateOrderRequest request);
        Task<ResponseDTO> GetOrdersByUserIdAsync(string userId);
    }
}
