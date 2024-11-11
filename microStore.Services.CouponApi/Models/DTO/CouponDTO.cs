using System.ComponentModel.DataAnnotations;

namespace microStore.Services.CouponApi.Models.DTO
{
    public class CouponDTO
    {
        public int CouponId { get; set; }

        public string CouponCode { get; set; }

        public int DiscountAmount { get; set; }

        public int MinAmount { get; set; }

        public DateTime ExpirationDate { get; set; }

        public int ActivationCount { get; set; }
    }
}
