using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerce.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        [ForeignKey("User")]
        public string ApplicationUserId { get; set; }

        public ApplicationUser User { get; set; }

        [ForeignKey("Shipment")]
        public int ShipmentId { get; set; }

        public Shipment Shipment { get; set; }
    }
}