using AutoMapper;

using microStore.Services.ProductApi.Models;
using microStore.Services.ProductApi.Models.DTO;

namespace microStore.Services.ProductApi
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<ProductDTO, Product>();
            CreateMap<Product, ProductDTO>();
            CreateMap<Product, ProductResponseDTO>()
            .ForMember(dest => dest.Brand, act => act.Ignore())
            .ForMember(dest => dest.CategoryProduct, opt => opt.MapFrom(src => src.Categories
                .Select(category => new CategoryProductDTO
                {
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName
                })));
            CreateMap<Brand, BrandDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ForMember(dest => dest.ProductIds, act => act.Ignore()).ReverseMap().ForMember(dest => dest.Products, act => act.Ignore());
            CreateMap<Category, CategoryReadDTO>().ReverseMap().ForMember(dest => dest.Products, act => act.Ignore());
            CreateMap<object, ProductDetailsDTO>();
            //CreateMap<PropertyType, PropertyTypeDTO>().ReverseMap().ForMember(dest => dest.Products, act => act.Ignore());
            //CreateMap<PropertyType, PropertyTypeDetailsDTO>().ReverseMap().ForMember(dest => dest.Products, act => act.Ignore());
            CreateMap<Property, PropertiesDTO>().ReverseMap().ForMember(dest => dest.PropertyTypeId, act => act.Ignore()); ;


        }
    }
}
