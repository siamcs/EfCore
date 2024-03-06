namespace EfCore.Models.ViewModel
{
    public class CreateProductViewModel
    {
        public int ProductId { get; set; }
        public string? Title { get; set; }
        public IFormFile? UrlImage { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
    }
}
