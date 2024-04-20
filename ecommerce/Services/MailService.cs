using ecommerce.Models;
using ecommerce.Settings;
using MailKit;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Net;
using System.Net.Mail;
using MailKit.Net.Smtp;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using ecommerce.TempelateMails;
using ecommerce.ViewModels;
namespace ecommerce.Services
{

  
    public class MailService : IMailService 
    {
        private readonly MailSettings _mailsettings;

        public MailService(IOptions<MailSettings> mailsettings)
        {
           _mailsettings = mailsettings.Value;   
        }
        public async Task SendEmailAsync(MailRequest mailrequest, mailTemplate mailTemplate, MailAdditionalParamsViewModel? additionalParams = null) 
        { 
            MimeMessage mail = new MimeMessage();
            mail.Sender = MailboxAddress.Parse(_mailsettings.Mail);
            mail.To.Add(MailboxAddress.Parse(mailrequest.ToEmail));     
            mail.Subject = mailrequest.Subject; 
            BodyBuilder builder = new BodyBuilder();

            if (mailrequest.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in mailrequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }
            builder.HtmlBody = mailTemplate.htmlTags(additionalParams);  

            mail.Body = builder.ToMessageBody();
            //SmtpClient smtp = new SmtpClient();
            //smtp.EnableSsl = true;
            //SecureSocketOptions secureSocketOptions = secureSocketOptions.sslon;
            //smtp.Host = _mailsettings.Host;
            //smtp.Port = _mailsettings.Port;
            //smtp.Credentials = new NetworkCredential(_mailsettings.Mail , _mailsettings.Password);
            //await smtp.SendAsync(mail); 

            using(MailKit.Net.Smtp.SmtpClient smtp = new MailKit.Net.Smtp.SmtpClient())
            {
                smtp.ServerCertificateValidationCallback = (s, c, h, e) => true; // Accept any certificate
                smtp.CheckCertificateRevocation = false; // Do not check certificate revocation
                smtp.Connect(_mailsettings.Host, _mailsettings.Port, SecureSocketOptions.Auto); // Use SecureSocketOptions.Auto for better security
                smtp.Authenticate(_mailsettings.Mail, _mailsettings.Password);
                await smtp.SendAsync(mail);
                smtp.Disconnect(true);
            }
        }
    }
}
