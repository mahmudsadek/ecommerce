using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerce.Models
{
    [Table("Order")]
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Date)]  // In this case we only want to keep track of the date, not the date and time
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]  // specifies that the formatting should also be applied when the value is displayed in a text box for editing
        public DateTime OrderDate { get; set; }

        public List<OrderItem>? OrderItems { get; set; }

        // --------------------------------------------

        [ForeignKey("User")]
        public string ApplicationUserId { get; set; }

        public ApplicationUser? User { get; set; }

        // --------------------------------------------

        [ForeignKey("Shipment")]
        public int? ShipmentId { get; set; }

        public Shipment? Shipment { get; set; }

        // Omar : New Update
    }
}