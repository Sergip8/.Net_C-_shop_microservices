using microStore.Services.ShoppingCartApi.Models.DTO;
using microStore.Services.ShoppingCartApi.Service.IService;
using Newtonsoft.Json;
using System.Text;

namespace microStore.Services.ShoppingCartApi.Service
{
    public class CouponService : ICouponService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CouponService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<CouponDTO> GetCoupon(string couponCode)
        {
            var client = _httpClientFactory.CreateClient("Coupon");

            var res = await client.GetAsync($"api/CouponApi/code/{couponCode}");
            var apiContent = await res.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ResponseDTO>(apiContent);
            if (response.Success)
            {
                return JsonConvert.DeserializeObject<CouponDTO>(Convert.ToString(response.Data));
            }
            return new CouponDTO();
        }
    }
}
