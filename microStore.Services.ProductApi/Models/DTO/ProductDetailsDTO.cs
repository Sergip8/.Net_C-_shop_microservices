using InventoryServiceClient;

namespace microStore.Services.ProductApi.Models.DTO
{
    public class ProductDetailsDTO
    {
        public ProductBaseDTO Product { get; set; }
        public BrandDTO Brand { get; set; }
        //public CategoryProductDTO Category { get; set; }
        public IEnumerable<ImageProductDTO> Images { get; set; }
        public InventoryDTO Inventory { get; set; }
        public CommentHeaderDTO Comment { get; set; }
        //public IEnumerable<PropertyTypeDetailsDTO> Properties { get; set; }

        public List<GroupedProperties> Properties { get; set; }

    }
    public class ProductDetailsDTOSpe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public decimal Current_price { get; set; }
        public decimal Previous_price { get; set; }
        public BrandDTO Brand { get; set; }
        public ICollection<ImageProductDTO> Images { get; set; }
        public ICollection<CategoryDTO> Categories { get; set; }

        public ProductAvailabilityWithId Inventory { get; set; }
        public ICollection<PropertyValuesDTO> Properties { get; set; }

    }
    public class ProductBaseDTO
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
    }
    public class PropertyTypeDetailsDTO
    {
        public int PropertyTypeId { get; set; }
        public string PropertyTypeName { get; set; }
        public IEnumerable<PropertiesDTO> Properties { get; set; }

    }
    public class PropertyValuesDTO
    {
        public int Id { get; set; } 
        public string PropertyValueName { get; set; }
        

        public PropertiesDTO property { get; set; }

    }
    public class PropertiesDTO
    {
        public string PropertyName { get; set; }
        public bool IsPrincipal { get; set; }
        public PropertyTypeDTO PropertyType { get; set; }
    }
    public class PropertyTypeDTO
    {
        public string PropertyTypeName { get; set; }
        public string PropertyTypeDescription { get; set; }
        public int PropertyTypeOrder { get; set; }

    }

    public class GroupedProperties
    {
        public PropertyTypeDTO PropertyType { get; set; }
        public List<Value> Values { get; set; }
    }

    public class Value
    {
        public string PropertyName { get; set; }
        public string PropertyValueName { get; set; }
    }

}
