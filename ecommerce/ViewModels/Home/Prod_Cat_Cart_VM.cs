using ecommerce.Models;

namespace ecommerce.ViewModels.Home
{
	public class Prod_Cat_Cart_VM
	{
        public List<Models.Product> Products { get; set; }

        public List<ecommerce.Models.Category> Categories { get; set; }


        public Cart? Cart { get; set; }
    }
}
