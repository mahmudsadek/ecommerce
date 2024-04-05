using ecommerce.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ecommerce.ViewModels.Product
{
	public class Products_With_CategoriesVM
	{
        public List<ecommerce.Models.Product> Products { get; set; }

        public List<Category> Categories { get; set; }
    }
}
