namespace microStore.Services.OrderApi.Models.DTO
{
    public class OrderResponse
    {
        public int OrderHeaderId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CouponCode { get; set; }
        public decimal? Discount { get; set; }
        public OrderStatus Status { get; set; }
        public decimal OrderTotal { get; set; }
        public DateTime OrderTime { get; set; }
        public PaymentResponse Payment { get; set; }
        public ShippingResponse Shipping { get; set; }
        public List<OrderDetailResponse> OrderDetails { get; set; }
    }

    public class PaymentResponse
    {
        public int PaymentId { get; set; }
        public PaymentMethod Method { get; set; }
        public decimal Amount { get; set; }
        public PaymentStatus Status { get; set; }
        public DateTime? PaidAt { get; set; }
    }

    public class ShippingResponse
    {
        public int ShippingId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public ShippingStatus Status { get; set; }
        public DateTime? ShippedAt { get; set; }
        public DateTime? DeliveredAt { get; set; }
    }

    public class OrderDetailResponse
    {
        public int OrderDetailsId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
