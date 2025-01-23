namespace microStore.Services.AuthApi.Models;

public class RecentlyViewedProduct
{
    public int Id { get; set; } // ID único del registro

    public string UserId { get; set; } // ID del usuario
    public ApplicationUser User { get; set; } // Relación con el usuario

    public int ProductId { get; set; } // ID del producto

    public DateTime ViewedAt { get; set; } = DateTime.UtcNow;
}