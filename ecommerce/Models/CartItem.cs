using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerce.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        //------------------------------------

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public  Product? Product { get; set; }


        [ForeignKey("Cart")]
        public int? CartId { get; set; }

        public  Cart? Cart { get; set; }
    }
}