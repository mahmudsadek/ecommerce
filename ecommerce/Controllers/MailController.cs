using ecommerce.Models;
using ecommerce.Services;
using ecommerce.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Controllers
{
    public class MailController : Controller
    {
        private readonly IMailService mailService;

       
        public MailController(IMailService _mailService)
        {
            mailService = _mailService;
        }

       
        public async Task <IActionResult> SendMail(string emailTo , string callBackUrl, string userName) // try recieve MailRequest mailrequest instead // saeed
        {
            MailRequest mailRequest = new MailRequest() {ToEmail = emailTo , Subject = "Reset password"};
            try
            {
               await mailService.SendEmailAsync(mailRequest, new MailAdditionalParamsViewModel()
               {callBackUrl = callBackUrl, userName = userName });
                return View("confirmSendingForgetPasswordEmail"); 
            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }
        }
    }
}
