using ecommerce.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerce.ViewModels.CartItem
{
    public class CartItemViewModel
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        //------------------------------------

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public ecommerce.Models.Product? Product { get; set; }

        [ForeignKey("Cart")]
        public int? CartId { get; set; }

        public Cart? Cart { get; set; }

    }
}
