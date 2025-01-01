using System.ComponentModel.DataAnnotations;

namespace microStore.Services.CouponApi.Models
{
    public class Coupon
    {
        [Key]
        public int CouponId { get; set; }
        [Required]
        public string CouponCode { get; set; }
        [Required]
        public int DiscountAmount { get; set; }
        [Required]
        public int MinAmount { get; set; }
        [Required]
        public DateTime ExpirationDate { get; set; }
        [Required]
        public int ActivationCount { get; set; }
        public int MaxActivations { get; set; }

    }
}
