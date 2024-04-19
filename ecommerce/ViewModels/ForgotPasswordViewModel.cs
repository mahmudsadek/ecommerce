using System.ComponentModel.DataAnnotations;

namespace ecommerce.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required , RegularExpression(@"[a-zA-Z0-9]+@[a-zA-Z0-9]+\.(com){1}\s*$")]
        public string? Email { get; set; }
        public bool IsEmailSent { get; set; }
    }
}
