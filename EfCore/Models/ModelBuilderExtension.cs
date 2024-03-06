using Microsoft.EntityFrameworkCore;

namespace EfCore.Models
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Electronics" },
                 new Category { CategoryId = 2, CategoryName = "Home Appliance" },
                 new Category { CategoryId = 3, CategoryName = "Cosmatics" }
                );
        }
    }
}
