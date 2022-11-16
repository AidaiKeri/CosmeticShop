using CosmeticShop.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace CosmeticShop.Model.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            IConfiguration config = (IConfiguration)new ConfigurationBuilder()
                .AddJsonFile("AppSettings.json")
                .Build();

            string connectionString = config.GetConnectionString("ConnectionString");

            builder.UseSqlServer(connectionString);
        }
    }
}
