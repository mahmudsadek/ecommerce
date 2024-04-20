using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerce.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [DisplayFormat(NullDisplayText = "No Description yet")]
        public string? Description { get; set; }

        public string ImageUrl { get; set; } = "wwwroot/images/CatDefault.png";

        [NotMapped]
        public IFormFile image { get; set; }
        public List<Product>? Products { get; set; }
    }
}

