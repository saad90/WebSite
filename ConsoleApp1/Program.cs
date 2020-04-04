using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Example
    {
        private static void Main()
        {
            Execute().Wait();
        }

        static async Task Execute()
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("benmakhloufsaad@gmail.com", "benmakhlouf saad");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress("saad907@maildrop.cc", "saad90");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}