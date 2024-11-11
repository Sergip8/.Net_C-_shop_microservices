using microStore.Services.ShoppingCartApi.Models.DTO;
using microStore.Services.ShoppingCartApi.Service.IService;
using Newtonsoft.Json;
using System.Text;


namespace microStore.Services.ShoppingCartApi.Service
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IEnumerable<ProductDTO>> GetProductsAsync(List<int> ids)
        {
            var client = _httpClientFactory.CreateClient("Product");
            var productIds = new ProductIdsResponse()
            {
                ProductIds = ids
            };
            using StringContent jsonContent = new(
       System.Text.Json.JsonSerializer.Serialize(new

       {
           ProductIds = ids

       }),
       Encoding.UTF8,
       "application/json");


            var res = await client.PostAsync($"api/ProductApi/ProductList", jsonContent);
            var apiContent = await res.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ResponseDTO>(apiContent);
            if (response.Success)
            {
                return JsonConvert.DeserializeObject<IEnumerable<ProductDTO>>(Convert.ToString(response.Data));
            }
            return new List<ProductDTO>();
        }

    }
}
