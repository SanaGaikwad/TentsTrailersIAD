using System;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace TentsTrailersIAD.Utils
{
    public class EmailSender
    {
        // Please use your API KEY here.
        private const String API_KEY = "";  
        public void Send(String toEmail, String subject, String contents)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("noreply@localhost.com", "Tents&Trailers");
            var to = new EmailAddress(toEmail, "");
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = client.SendEmailAsync(msg);
        }
    }
    
}