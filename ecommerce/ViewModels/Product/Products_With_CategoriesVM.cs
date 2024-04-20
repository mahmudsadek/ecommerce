using ecommerce.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ecommerce.ViewModels.Product
{
	public class Products_With_CategoriesVM
	{
        public List<Models.Product> Products { get; set; }

        public List<ecommerce.Models.Category> Categories { get; set; }


        public Cart? Cart { get; set; } //= new Cart() { CartItems = new List<CartItem>()};
    }
}
