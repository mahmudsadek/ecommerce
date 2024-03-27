using System.ComponentModel.DataAnnotations.Schema;

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

        //---------------------------------

        [ForeignKey("order")]
        public int OrderId { get; set; }

        public Order Order { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
