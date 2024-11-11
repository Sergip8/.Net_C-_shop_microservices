namespace microStore.Services.ProductApi.Models.DTO
{
    public class ProductDetailsDTO
    {
        public ProductBaseDTO Product { get; set; }
        public BrandDTO Brand { get; set; }
        //public CategoryProductDTO Category { get; set; }
        public PriceRangeDTO Price { get; set; }
        public IEnumerable<ImageProductDTO> Images { get; set; }
        public InventoryDTO Inventory { get; set; }
        public CommentHeaderDTO Comment { get; set; }
        //public IEnumerable<PropertyTypeDetailsDTO> Properties { get; set; }

        public List<GroupedProperties> Properties { get; set; }

    }
    public class ProductBaseDTO
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
    }
    public class PriceRangeDTO
    {

        public decimal HighPrice { get; set; }
        public decimal LowPrice { get; set; }
    }
    public class PropertyTypeDetailsDTO
    {
        public int PropertyTypeId { get; set; }
        public string PropertyTypeName { get; set; }
        public IEnumerable<PropertiesDTO> Properties { get; set; }

    }
    public class PropertiesDTO
    {
        public int PropertyId { get; set; }
        public string PropertyValue { get; set; }
        public string PropertyName { get; set; }

    }
    public class PropertyTypeDTO
    {
        public string PropertyTypeName { get; set; }
        public int PropertyTypeId { get; set; }
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
