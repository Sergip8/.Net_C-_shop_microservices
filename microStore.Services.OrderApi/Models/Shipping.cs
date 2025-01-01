using System.ComponentModel.DataAnnotations;

namespace microStore.Services.OrderApi.Models
{
    public class Shipping
    {
        [Key]
        public int ShippingId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public ShippingStatus Status { get; set; }
        public DateTime? ShippedAt { get; set; }
        public DateTime? DeliveredAt { get; set; }
    }

    public enum ShippingStatus
    {
        Pending,
        Shipped,
        Delivered,
        Cancelled
    }
}
