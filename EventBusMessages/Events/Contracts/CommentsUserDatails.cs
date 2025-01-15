namespace EventBusMessages.Events.Contracts
{
    public class ValidateUserRequest
    {
        public string UserId { get; set; }
    }

    public class ValidateUserResponse
    {
        //public string UserId { get; set; }
        public bool IsValid { get; set; }
    }

    public class GetUserDetailsRequest
    {
        public List<string> UserId { get; set; }
    }
    public class GetUserDetailsResponse
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
    public class GetUserDetailsResponseList
    {
        public List<GetUserDetailsResponse> UserDetails { get; set; }
    }
    public class ValidateInventoryRequest
    {
        public List<OrderProduct> Products { get; set; }
    }
    public class OrderProduct
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }

    }
    public class ValidateInventoryResponse
    {
        public List<ValidateInventory> InventoryResponse { get; set; }
    }
    public class ValidateInventory
    {
        public int ProductId { get; set; }
        public bool IsValid { get; set; }
    }
    public class CreateInventory
    {
        public int Quantity { get; set; }
        public decimal RetailPrice { get; set; }
        public int VendorId { get; set; }
        public int ProductId { get; set; }

    }
}
