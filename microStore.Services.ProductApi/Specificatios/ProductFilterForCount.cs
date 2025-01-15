using microStore.Services.ProductApi.Models;

namespace microStore.Services.ProductApi.Specificatios;

public class ProductFilterForCount:  BaseSpecification<Product>
{
    public ProductFilterForCount(ProductSpecificationParams productParams)
        : base(p =>
                (string.IsNullOrEmpty(productParams.Search) || p.Name.ToLower().Contains(productParams.Search)) &&
                (!productParams.BrandId.HasValue || p.BrandId == productParams.BrandId) 
                &&(productParams.PropertiesId.Count == 0 || p.Properties.Any(pro => productParams.PropertiesId.Contains(pro.Id))) 
            //&& p.IsAvailable == true
        )
    {
    }
}