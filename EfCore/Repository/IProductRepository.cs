using EfCore.Models;

namespace EfCore.Repository
{
    public interface IProductRepository
    {
        Product GetProductById(int id);
        IEnumerable<Product> GetAllProducts();
        Product SaveProducts(Product products);
        Product UpdateProduct(Product product);
        void DeleteProduct(int id);
        IEnumerable<Category> GetAllCategories();
    }
}
