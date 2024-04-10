using ecommerce.Models;
using ecommerce.ViewModels;

namespace ecommerce.Services
{
    public interface IMailService
    {
        public Task SendEmailAsync(MailRequest mailrequest , MailAdditionalParamsViewModel? additionalParams); 

    }
}
