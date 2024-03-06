using EfCore.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace EfCore.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDBContext _db;
        private readonly IHostingEnvironment _environment;

        public ProductRepository(AppDBContext db, IHostingEnvironment environment)
        {
            _db = db;
            _environment = environment;
        }
        public Product GetProductById(int id)
        {
            Product product = _db.Products.FirstOrDefault(p => p.ProductId == id);
            return product;
        }
        public void DeleteProduct(int id)
        {
            Product product = GetProductById(id);
            _db.Products.Remove(product);
            _db.SaveChanges();
        }

        public IEnumerable<Category> GetAllCategories()
        {
            IEnumerable<Category> categories = _db.Categories.ToList();
            return categories;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var data = _db.Products.Select(p => new Product
            {
                ProductId = p.ProductId,
                Title = p.Title,
                Price = p.Price,
                UrlImage = p.UrlImage,
                CategoryId = p.CategoryId,
                Category = _db.Categories.Where(c => c.CategoryId == p.CategoryId).FirstOrDefault(),
            }).ToList();
            return data;
        }
        public Product SaveProducts(Product products)
        {
            _db.Products.Add(products);
            _db.SaveChanges();
            return products;
        }
        public Product UpdateProduct(Product upProduct)
        {
            var product = _db.Products.Attach(upProduct);
            product.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();
            return upProduct;
        }
    }
}
