using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerce.Models
{
    [Table("OrderItem")]
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        public int Quantity { get; set; }

        //----------------------------------

        [ForeignKey("Product")]
        public int ProductId { get; set; } 

        public  Product Product { get; set; }

        //----------------------------------

        [ForeignKey("Order")]
        public int OrderId { get; set; }

        public Order Order { get; set; }
    }
}
