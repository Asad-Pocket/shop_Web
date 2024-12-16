using Microsoft.EntityFrameworkCore;
using shop_Web.Models;
using static System.Net.Mime.MediaTypeNames;

namespace shop_Web.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> option) : base(option)
        {

        }
        public DbSet<Category> category { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<Card> Card { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasKey(c => c.cat_id); // Define cat_id as the primary key
            modelBuilder.Entity<Item>()
                .HasKey(c => c.item_id); // Define item_id as the primary key
            modelBuilder.Entity<Card>()
               .HasKey(c => c.card_id); // Define item_id as the primary key
        }
    }
}
