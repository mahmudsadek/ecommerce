using Microsoft.AspNetCore.Identity;

namespace ecommerce.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Shipment>? Shipments { get; set; }
        public List<Order>? Orders { get; set;}
    }
}

// comment from omar to test changes tracking
// comment from maher to everyone
