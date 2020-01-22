using Microsoft.EntityFrameworkCore;

namespace blogapi.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            //Database.EnsureCreated();
            //Database.Migrate();
        }

        public DbSet<Post> Posts { get; set; }

        public DbSet<User> Users { get; set; }
    }
}