using System.ComponentModel.DataAnnotations;

namespace microStore.Services.OrderApi.Models
{
    public class OrderHeader
    {
        [Key]
        public int OrderHeaderId { get; set; }
        public string? UserId { get; set; }
        public string? CouponCode { get; set; }
        public decimal? Discount { get; set; }
        public decimal OrderTotal { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public int PaymentId { get; set; }
        public int ShippingId { get; set; }
        public DateTime OrderTime { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public Payment Payment { get; set; }
        public Shipping Shipping { get; set; }
        public IEnumerable<OrderDetails> OrderDetails { get; set; }
    }
    public enum OrderStatus
    {
        Pending,
        Processing,
        Shipped,
        Completed,
        Cancelled
    }
}
