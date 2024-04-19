using System.ComponentModel.DataAnnotations;

namespace ecommerce.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [DisplayFormat(NullDisplayText = "No Description yet")]
        public string? Description { get; set; }

        public string ImageUrl { get; set; } = "wwwroot/images/CatDefault.png";
        public List<Product>? Products { get; set; }
    }
}

