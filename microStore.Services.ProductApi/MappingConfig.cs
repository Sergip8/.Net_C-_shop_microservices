﻿using AutoMapper;
using InventoryServiceClient;
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
            CreateMap<ProductBasicDTO, Product>();
            CreateMap<Product, ProductBasicDTO>();
            CreateMap<Product, ProductDetailsDTOSpe>();
            CreateMap<ProductDetailsDTOSpe, ProductBasicDTO>();
            CreateMap<ProductImages, ImageProductDTO>();
            CreateMap<PropertyValue, PropertyValuesDTO>();
            CreateMap<Property, PropertiesDTO>();
            CreateMap<PropertyType, PropertyTypeDTO>();
            CreateMap<ProductAvailabilityWithId, ProductAvailability>().ReverseMap();



            CreateMap<Product, ProductResponseDTO>()
            .ForMember(dest => dest.Brand, act => act.Ignore())
            .ForMember(dest => dest.CategoryProduct, opt => opt.MapFrom(src => src.Categories
                .Select(category => new CategoryProductDTO
                {
                    CategoryId = category.Id,
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
