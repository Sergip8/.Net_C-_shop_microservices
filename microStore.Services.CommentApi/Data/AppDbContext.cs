
using Microsoft.EntityFrameworkCore;
using microStore.Services.CommentApi.Models;


namespace microStore.Services.CommentApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<CommentHeader> CommentHeader { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // base.OnModelCreating(modelBuilder); 

            modelBuilder.Entity<CommentHeader>().HasData(new CommentHeader
            {
                CommentHeaderId = 1,
                OverallScore = 2f,
                QtyForStar = "",
                CommentCount = 7,
                ProductId = 3
            }); modelBuilder.Entity<Comment>().HasData(new Comment
            {
                CommentId = 1,
                Title = "Comment",
                Content = "Una mierda pinchada en un palo",
                Score = 1,
                Votes = 0,
                CommentUserId = "d3733ffc-aafe-448f-bdb5-10b18c941a15",
                CommentHeaderId = 1
            }); modelBuilder.Entity<Comment>().HasData(new Comment
            {
                CommentId = 2,
                Title = "Comment",
                Content = "Una mierda pinchada en un palo",
                Score = 1,
                Votes = 0,
                CommentUserId = "d3733ffc-aafe-448f-bdb5-10b18c941a15",
                CommentHeaderId = 1
            }); modelBuilder.Entity<Comment>().HasData(new Comment
            {
                CommentId = 3,
                Title = "Comment",
                Content = "Una mierda pinchada en un palo",
                Score = 1,
                Votes = 0,
                CommentUserId = "d3733ffc-aafe-448f-bdb5-10b18c941a15",
                CommentHeaderId = 1
            }); modelBuilder.Entity<Comment>().HasData(new Comment
            {
                CommentId = 4,
                Title = "Comment",
                Content = "Una mierda pinchada en un palo",
                Score = 1,
                Votes = 0,
                CommentUserId = "d3733ffc-aafe-448f-bdb5-10b18c941a15",
                CommentHeaderId = 1
            }); modelBuilder.Entity<Comment>().HasData(new Comment
            {
                CommentId = 5,
                Title = "Comment",
                Content = "Una mierda pinchada en un palo",
                Score = 1,
                Votes = 0,
                CommentUserId = "d3733ffc-aafe-448f-bdb5-10b18c941a15",
                CommentHeaderId = 1
            }); modelBuilder.Entity<Comment>().HasData(new Comment
            {
                CommentId = 6,
                Title = "Comment",
                Content = "Una mierda pinchada en un palo",
                Score = 1,
                Votes = 0,
                CommentUserId = "d3733ffc-aafe-448f-bdb5-10b18c941a15",
                CommentHeaderId = 1
            }); modelBuilder.Entity<Comment>().HasData(new Comment
            {
                CommentId = 7,
                Title = "Comment",
                Content = "Una mierda pinchada en un palo",
                Score = 1,
                Votes = 0,
                CommentUserId = "d3733ffc-aafe-448f-bdb5-10b18c941a15",
                CommentHeaderId = 1
            });
        }
    }
}
