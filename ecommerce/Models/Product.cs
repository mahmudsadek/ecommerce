using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerce.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [DisplayFormat(NullDisplayText = "No Description yet")]
        public string? Description { get; set; }

        public string ImageUrl { get; set; } = "wwwroot/images/sfdsdf";  // default image

        [Column(TypeName = "Money")]
        public decimal Price { get; set; }

        public int Quantity { get; set;}

        public string Color { get; set; }

        // we want to make array of colors for each product
        //public int MyProperty { get; set; }

        [DisplayFormat(NullDisplayText = "No Rating yet")]
        [Column(TypeName = "Money")]

        public decimal? Rating { get; set; }

        //----------------------------------

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public List<Comment>? Comments { get; set; }
    }
}
