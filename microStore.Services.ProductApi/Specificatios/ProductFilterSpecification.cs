using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using microStore.Services.ProductApi.Models;

namespace microStore.Services.ProductApi.Specificatios
{
    public class ProductFilterSpecification : BaseSpecification<Product>
    {

        public ProductFilterSpecification(ProductSpecificationParams productParams) : base(p =>
            (string.IsNullOrEmpty(productParams.Search) || p.Name.ToLower().Contains(productParams.Search)) &&
            (!productParams.BrandId.HasValue || p.BrandId == productParams.BrandId) 
            &&(productParams.PropertiesId.Count == 0 || p.Properties.Any(pro => productParams.PropertiesId.Contains(pro.Id))) 
            //&& p.IsAvailable == true
        )
        {
            // Includes
            AddInclude(p => p.Brand);
            AddInclude(p => p.Categories);
            AddInclude(p => p.Images);
            AddInclude($"{nameof(Product.Properties)}.{nameof(PropertyValue.Property)}");

            // Paging
            ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

            // Sorting
            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case "nameAsc":
                        ApplyOrderBy(p => p.Name);
                        break;
                    case "nameDesc":
                        ApplyOrderByDescending(p => p.Name);
                        break;
                    case "brandAsc":
                        ApplyOrderBy(p => p.Brand.BrandName);
                        break;
                    case "brandDesc":
                        ApplyOrderByDescending(p => p.Brand.BrandName);
                        break;
                    case "priceAsc":
                        ApplyOrderBy(p => p.Current_price);
                        break;
                    case "priceDesc":
                        ApplyOrderByDescending(p => p.Current_price);
                        break;
                    case "idAsc":
                        ApplyOrderBy(p => p.Id);
                        break;
                    case "idDesc":
                        ApplyOrderByDescending(p => p.Id);
                        break;
                    default:
                        ApplyOrderBy(p => p.Name);
                        break;
                }
            }
            else
            {
                ApplyOrderBy(p => p.Name);
            }
        }

        public ProductFilterSpecification(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.Brand);
            AddInclude(p => p.Categories);
            AddInclude(p => p.Images);
            //AddInclude(p => p.Properties);
            //AddThenInclude(q => q.Include(p => p.Properties).ThenInclude(e => e.Property));
            AddInclude($"{nameof(Product.Properties)}.{nameof(PropertyValue.Property)}.{nameof(Property.PropertyType)}");
        }
        /*public ProductFilterSpecification(string link) : base(p => p.Link == link)
        {
            AddInclude(p => p.Brand);
            AddInclude(p => p.Categories);
            AddInclude(p => p.Images);
            //AddInclude(p => p.Properties);
            //AddThenInclude(q => q.Include(p => p.Properties).ThenInclude(e => e.Property));
            AddInclude($"{nameof(Product.Properties)}.{nameof(PropertyValue.Property)}.{nameof(Property.PropertyType)}");
        }*/
        public ProductFilterSpecification() : base()
        {
            AddInclude(p => p.Brand);
            AddInclude(p => p.Images);

            ApplyPaging(0, 5);


        }
        public ProductFilterSpecification(int page, int size ) : base()
        {
            AddInclude(p => p.Brand);
            AddInclude(p => p.Categories);
            AddInclude(p => p.Images);
            AddInclude(p => p.Properties);
            ApplyPaging(page, size);


        }
    }
    public class ProductSpecificationParams
    {
        private const int MaxPageSize = 24;
        public int PageIndex { get; set; } = 1;

        private int _pageSize = 14;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public int? BrandId { get; set; }
        public List<int> PropertiesId { get; set; } = new List<int>();
        public string? Sort { get; set; }
        private string _search = "";
        public string? Search
        {
            get => _search;
            set => _search = value.ToLower();
        }

        public bool IsAvailable { get; set; } = true;
    }
}
