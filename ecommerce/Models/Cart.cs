using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerce.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public List<CartItem>? CartItems { get; set; }

        //----------------------------------------------
        // Omar : I don't know should the cart be realted to each user or not considering that any one can add to cart without registering

        //[ForeignKey("User")]
        //public string ApplicationUserId { get; set; }

        //public ApplicationUser User { get; set; }

    }
}
