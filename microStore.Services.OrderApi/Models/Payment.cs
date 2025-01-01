using System.ComponentModel.DataAnnotations;

namespace microStore.Services.OrderApi.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        public PaymentMethod Method { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaidAt { get; set; }
        public PaymentStatus Status { get; set; }
    }

    public enum PaymentMethod
    {
        CreditCard,
        DebitCard,
        BankTransfer,
        CashOnDelivery
    }

    public enum PaymentStatus
    {
        Pending,
        Paid,
        Failed,
        Refunded
    }
}
