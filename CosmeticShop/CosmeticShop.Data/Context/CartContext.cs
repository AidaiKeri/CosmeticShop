using Microsoft.EntityFrameworkCore;
using CosmeticShop.Model.Entities;

namespace CosmeticShop.Data.Context
{
    public class CartContext : DbContext
    {
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<CartProduct> Products { get; set; }
        public DbSet<CartUser> Users { get; set; }
        protected override void OnConfiguring
            (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "CartDb");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartItem>()
                .HasKey(sk => sk.Id);

            modelBuilder.Entity<CartItem>()
                .HasOne(sk => sk.User)
                .WithMany(sk => sk.CartItems)
                .HasForeignKey(sk => sk.UserId);

            modelBuilder.Entity<CartItem>()
                .HasOne(sk => sk.Product)
                .WithMany(sk => sk.CartItems)
                .HasForeignKey(sk => sk.ProductId);

            base.OnModelCreating(modelBuilder);
        }
    }

}
