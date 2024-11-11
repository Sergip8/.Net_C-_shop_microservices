using Microsoft.EntityFrameworkCore;
using microStore.Services.ShoppingCartApi.Models;


namespace microStore.Services.ShoppingCartApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<CartDetails>? CartDetails { get; set; }
        public DbSet<CartHeader>? CartHeaders { get; set; }


    }
}
