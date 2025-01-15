using EventBusMessages.Events.Contracts;

namespace microStore.Services.OrderApi.Models.DTO
{
    public class CreateOrderRequest
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CouponCode { get; set; }
        public decimal? Discount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string ShippingAddress { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingPostalCode { get; set; }
        public string ShippingCountry { get; set; }
        public List<OrderDetailRequest> OrderDetails { get; set; }
    }
    public class OrderDetailRequest
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }

    }
}
