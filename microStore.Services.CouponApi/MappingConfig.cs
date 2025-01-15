using AutoMapper;
using microStore.Services.CouponApi.Models;
using microStore.Services.CouponApi.Models.DTO;

namespace microStore.Services.CouponApi
{
    public class CouponMappingProfile : Profile
    {
        public CouponMappingProfile()
        {
            CreateMap<CouponDTO, Coupon>();
            CreateMap<Coupon, CouponDTO>();
        }
    }
}
