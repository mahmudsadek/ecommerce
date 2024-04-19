using ecommerce.Models;
using ecommerce.TempelateMails;
using ecommerce.ViewModels;

namespace ecommerce.Services
{
    public interface IMailService
    {
        public Task SendEmailAsync(MailRequest mailrequest , mailTemplate mailTemplate, MailAdditionalParamsViewModel? additionalParams); 

    }
}
