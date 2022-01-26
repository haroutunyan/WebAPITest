using Domain.Categories.Models;
using Domain.Users.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class AppContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }

        public AppContext(DbContextOptions<AppContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            Category.Configure(modelBuilder);
            User.Configure(modelBuilder);
        }
    }
}