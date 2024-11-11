using microStore.Services.ShoppingCartApi.Models.DTO;

namespace microStore.Services.ShoppingCartApi.Service.IService
{
    public interface ICouponService
    {
        Task<CouponDTO> GetCoupon(string couponCode);
    }
}
