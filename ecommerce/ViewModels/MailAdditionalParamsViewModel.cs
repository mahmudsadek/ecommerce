using System.ComponentModel.DataAnnotations;

namespace ecommerce.ViewModels
{
    public class MailAdditionalParamsViewModel
    {
        public string? callBackUrl { get; set; }
        public string? userName { get; set; }
        public string? phoneNumber { get; set; }
        public string? Email { get; set; } 

        [DataType (DataType.Password)] 
        public string? password { get; set; }
        public string? confirmPassword { get; set; }


    }
}
