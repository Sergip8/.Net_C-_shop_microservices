using microStore.Services.ShoppingCartApi.Models.DTO;

namespace microStore.Services.ShoppingCartApi.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProductsAsync(List<int> ids);
    }
}
