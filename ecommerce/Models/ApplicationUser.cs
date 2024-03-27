using Microsoft.AspNetCore.Identity;

namespace ecommerce.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Shipment>? Shipments { get; set; }

        public List<Order>? Orders { get; set;}

        public List<Comment>? Comments { get; set; }
    }
}
