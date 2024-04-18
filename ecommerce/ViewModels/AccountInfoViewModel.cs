using System.ComponentModel.DataAnnotations;

namespace ecommerce.ViewModels
{
    public class AccountInfoViewModel       // as compiler dont want to access registerviewModel!!!!!!!!!!!!!!!!
    {
        [Required, MaxLength(20), MinLength(2, ErrorMessage = "User name must be at least 2 characters"), Display(Name = "User name")]
        public string userName { get; set; }

        [Required, DataType(DataType.Password), MinLength(6, ErrorMessage = "Password must be at least 6 characters")] 
        public string password { get; set; }

        [Required, Compare("password"), DataType(DataType.Password),
            Display(Name = "Confirm password")] 
        public string confirmPassword { get; set; }

        [Required, DataType(DataType.PhoneNumber), Display(Name = "Phone number"), RegularExpression("^[0]{1}[1]{1}[0-1-2-5]{1}[0-9]{8}$",
            ErrorMessage = "Invalid phone number")]
        public string phoneNumber { get; set; }

        [Required , RegularExpression(@"[a-zA-Z0-9]+@[a-zA-Z0-9]+\.(com){1}\s*$")] 
        public string? Email { get; set; }
    }
}
