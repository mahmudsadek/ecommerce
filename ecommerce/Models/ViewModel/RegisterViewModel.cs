using System.ComponentModel.DataAnnotations;

namespace ecommerce.Models.ViewModel
{
    public class RegisterViewModel
    {
        [Required, MaxLength(20), MinLength(2) , Display(Name = "User name")]
        public string userName { get; set; }

        [Required, DataType(DataType.Password)]
        public string password { get; set; }

        [Required, Compare("password"), DataType(DataType.Password),
            Display(Name = "Confirm password")]
        public string confirmPassword { get; set; }

        [Required , DataType(DataType.PhoneNumber), Display(Name ="Phone number")]  
        public string phoneNumber { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
