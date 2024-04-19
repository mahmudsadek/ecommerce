using ecommerce.ViewModels;

namespace ecommerce.TempelateMails
{
    public class mailTemplate
    {
        virtual public string htmlTags(MailAdditionalParamsViewModel additionalParams)
        {
            return "<h1> Hello from mail template <h1/>";
        }
        
    }
}
