using Microsoft.AspNetCore.Mvc;
using microStore.Services.ProductApi.Models.DTO;

namespace microStore.Services.ProductApi.Service.IService
{
    public interface IProductService
    {
        Task<ResponseDTO> GetPageableProducts(int page, int size);
        Task<ResponseResultsDTO> GetProductsResults(ProductRequestDTO requestDTO);
        Task<ResponseDTO> GetProductById(int productId);
        //Task<ResponseDTO> GetProductByLink(string productId);
        Task<ResponseDTO> GetHomeProducts();
        Task<ResponseDTO> GetProductByName(string search);
        Task<ResponseDTO> StoreProduct(ProductDTO productDTO);
        Task<ResponseDTO> UpdateProduct(ProductDTO productDTO);
        Task<ResponseDTO> DeleteProduct(int productId);
        Task<Object> UploadFile(IFormCollection form);
        Task<ResponseDTO> getProductsByIds(ProductIdsRequest ids);
        Task<ResponseDTO> GetProductDetails(int id);
    }
}
