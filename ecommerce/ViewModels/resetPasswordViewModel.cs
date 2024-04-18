using System.ComponentModel.DataAnnotations;

namespace ecommerce.ViewModels
{
    public class resetPasswordViewModel
    {
        [Required, Display(Name ="New Password") ,DataType(DataType.Password), MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string newPassword { get; set; }


        [Required, Compare("newPassword"), DataType(DataType.Password),
            Display(Name = "Confirm Password")]
        public string confirmNewPassword { get; set; }

        public string? userName { get; set; }

        public string? token { get; set; }
    }
}
