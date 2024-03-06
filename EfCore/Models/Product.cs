namespace EfCore.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string? Title { get; set; }
        public string? UrlImage { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }
    }
}
