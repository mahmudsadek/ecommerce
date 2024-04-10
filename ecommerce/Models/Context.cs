using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Models
{
    public class Context : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartItem> CartItem { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<Shipment> Shipment { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public Context() : base() { }

        public Context(DbContextOptions<Context> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().HasIndex(appUser => appUser.Email)
                .IsUnique();
        }
    }
}
