using Microsoft.EntityFrameworkCore;

namespace EfCore.Models
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(p => p.ProductId);
            modelBuilder.Entity<Category>().HasKey(p => p.CategoryId);
            modelBuilder.Entity<Product>().HasOne(s => s.Category).WithMany(s => s.Products).HasForeignKey(s => s.CategoryId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Seed();
        }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
    }
}
