namespace microStore.Services.AuthApi.Models;

public class Favorites
{
    public int Id { get; set; }

    public string UserId { get; set; }
    public ApplicationUser User { get; set; } 

    public int ProductId { get; set; } 

    public DateTime ViewedAt { get; set; } = DateTime.UtcNow;
}