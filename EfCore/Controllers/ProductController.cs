using EfCore.Models.ViewModel;
using EfCore.Models;
using EfCore.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace EfCore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _repository;
        private readonly IHostingEnvironment _environment;

        public ProductController(IProductRepository repository, IHostingEnvironment environment)
        {
            _repository = repository;
            _environment = environment;
        }

        public IActionResult Index()
        {
            List<Product> products = _repository.GetAllProducts().ToList();
            return View(products);
        }
        public IActionResult Create()
        {
            CategoryDropDownlist();
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateProductViewModel obj)
        {
            if (ModelState.IsValid)
            {
                string UrlImage = "";
                var files = HttpContext.Request.Form.Files;
                UrlImage = SaveImage(files);

                var data = new Product()
                {
                    Title = obj.Title,
                    UrlImage = UrlImage,
                    Price = obj.Price,
                    CategoryId = obj.CategoryId,
                };
                _repository.SaveProducts(data);
                return RedirectToAction(nameof(Index));

            }
            CategoryDropDownlist();
            return View();
        }
        private void CategoryDropDownlist(object CategorySelect = null)
        {
            var categories = _repository.GetAllCategories().ToList();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName", CategorySelect);
        }
        private string SaveImage(IFormFileCollection files)
        {
            string UrlImage = "";
            foreach (var image in files)
            {
                if (image != null && image.Length > 0)
                {
                    var file = image;
                    var uploads = Path.Combine(_environment.WebRootPath, "Images");
                    if (file.Length > 0)
                    {
                        var fileName = Guid.NewGuid().ToString().Replace("_", "") + file.FileName;
                        using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                            UrlImage = fileName;
                        }
                    }
                }

            }
            return UrlImage;
        }
  
        public IActionResult Edit(int id)
        {
            var product = _repository.GetProductById(id);
            CreateProductViewModel obj = new CreateProductViewModel();
            CategoryDropDownlist();
            return View(product);
        }
        [HttpPost]
        public IActionResult Edit(int id, CreateProductViewModel product)
        {
            {
                string UrlImage = "";
                var files = HttpContext.Request.Form.Files;
                UrlImage = SaveImage(files);
                var data = _repository.GetProductById(id);
                data.Title = product.Title;
                data.Price = product.Price;
                if (data.UrlImage != null)
                {
                    string fp = Path.Combine(_environment.WebRootPath, "Images", data.UrlImage);
                    System.IO.File.Delete(fp);
                }
                data.UrlImage = UrlImage;
                data.CategoryId = product.CategoryId;
                _repository.UpdateProduct(data);
                return RedirectToAction(nameof(Index));
            }
     }
    }
}
