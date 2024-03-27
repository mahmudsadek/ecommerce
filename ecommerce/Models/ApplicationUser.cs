using Microsoft.AspNetCore.Identity;

namespace ecommerce.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Shipment>? Shipments { get; set; }
        public List<Order>? Orders { get; set;}
    }
}

//<<<<<<< HEAD
//tesssssssssssssst omar new branch

//=======

// deleted from maher 
//>>>>>>> 78c72faa74c73230ffe75500e6d61fa44f3852eb
