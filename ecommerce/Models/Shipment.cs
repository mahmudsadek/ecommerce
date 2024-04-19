using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerce.Models
{
    [Table("Shipment")]
    public class Shipment
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Date)]  // In this case we only want to keep track of the date, not the date and time
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]  // specifies that the formatting should also be applied when the value is displayed in a text box for editing
        public DateTime? Date { get; set; }
      
        public string Address { get; set; }

        public string City { get; set; }

        public string Region { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        //---------------------------------

        [ForeignKey("order")]
        public int OrderId { get; set; }

        public Order? Order { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public ApplicationUser? User { get; set; }
    }
}
