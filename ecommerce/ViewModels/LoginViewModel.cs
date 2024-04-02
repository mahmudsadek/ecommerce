using System.ComponentModel.DataAnnotations;

namespace ecommerce.ViewModel 
{
    public class LoginViewModel
    {
        [Required, MaxLength(10), MinLength(3), Display(Name = "User name")]
        public string userName { get; set; }
        [Required, DataType(DataType.Password)]
        public string password { get; set; }

        [Display(Name = "remember me")]
        public bool rememberMe { get; set; }
    }
}
