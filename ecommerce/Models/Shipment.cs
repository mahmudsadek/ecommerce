namespace ecommerce.Models
{
    public class Shipment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        List<Order>? orders { get; set; }
    }
}
