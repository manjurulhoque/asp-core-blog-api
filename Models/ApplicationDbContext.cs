using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace blogapi.Models
{
    public class ApplicationDbContext : IdentityDbContext
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