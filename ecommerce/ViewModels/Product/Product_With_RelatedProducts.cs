using ecommerce.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace ecommerce.ViewModels.Product
{
    public class Product_With_RelatedProducts
    {
        public List<Models.Product> RealtedProducts { get; set; }

        public string CategoryName { get; set; }

        public Cart? Cart { get; set; } //= new Cart() { CartItems = new List<CartItem>()};

        public List<ecommerce.Models.Category> Categories { get; set; } // for the _AllProducts layout using it in the footer


        //====================================

        public int Id { get; set; }

        public string Name { get; set; }

        [DisplayFormat(NullDisplayText = "No Description yet")]
        public string? Description { get; set; }

        public string ImageUrl { get; set; } = "wwwroot/images/sfdsdf";  // default image

        [Column(TypeName = "Money")]
        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string Color { get; set; }

        // we want to make array of colors for each product
        //public int MyProperty { get; set; }

        [DisplayFormat(NullDisplayText = "No Rating yet")]
        [Column(TypeName = "Money")]
        public decimal? Rating { get; set; }

        //----------------------------------

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public ecommerce.Models.Category Category { get; set; }

        /// TODO: try to add categories prop because there is an error => Done :)

        public List<Comment>? Comments { get; set; }
    }
}
