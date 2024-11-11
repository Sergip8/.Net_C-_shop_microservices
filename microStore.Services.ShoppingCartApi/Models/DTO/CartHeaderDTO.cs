using System.ComponentModel.DataAnnotations.Schema;

namespace microStore.Services.ShoppingCartApi.Models.DTO
{
    public class CartHeaderDTO
    {
        public int CartHeaderId { get; set; }
        public string? UserId { get; set; }
        public string? CouponCode { get; set; }
        public double Discount { get; set; }
        public decimal CartTotal { get; set; }
    }
}
