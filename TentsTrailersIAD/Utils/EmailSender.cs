using System;
using System.IO;
using System.Web;
using SendGrid;
using SendGrid.Helpers.Mail;


namespace TentsTrailersIAD.Utils
{
    public class EmailSender
    {
        // Please use your API KEY here.
        private const String API_KEY = "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";
        public void Send(String fromEmail, String toEmail, String subject, String contents)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress(fromEmail,"");
            var to = new EmailAddress(toEmail, "");
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            var response = client.SendEmailAsync(msg);

        }
    }
}
