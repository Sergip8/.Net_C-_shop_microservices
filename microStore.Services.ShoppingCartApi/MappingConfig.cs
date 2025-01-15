using AutoMapper;
using microStore.Services.ShoppingCartApi.Models;
using microStore.Services.ShoppingCartApi.Models.DTO;


namespace microStore.Services.ShoppingCartApi
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<CartHeader, CartHeaderDTO>().ReverseMap();
            CreateMap<CartDetails, CartDetailsDTO>().ReverseMap();
        }
    }
}
