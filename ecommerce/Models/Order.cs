namespace ecommerce.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }

        List<OrderItem>? orderItems { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser User { get; set; }

        public int ShipmentId { get; set; }
        public Shipment Shipment { get; set; }
    }
}
// mahmoud sadek hu ya rgalah
