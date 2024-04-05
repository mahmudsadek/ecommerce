using ecommerce.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ecommerce.ViewModels.Product
{
    public class ProductWithCommentsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string Color { get; set; }
        public decimal? Rating { get; set; }

        public int CategoryId { get; set; }
        public List<Comment>? Comments { get; set; }

    }
}
