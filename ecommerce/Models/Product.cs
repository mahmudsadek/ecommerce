using System.ComponentModel.DataAnnotations;

namespace ecommerce.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set;}

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
