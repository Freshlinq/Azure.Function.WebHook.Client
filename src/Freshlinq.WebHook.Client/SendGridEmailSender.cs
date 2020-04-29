using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Freshlinq.WebHook.Client.Configuration;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Freshlinq.WebHook.Client
{
    public class SendGridEmailSender
    {
        private readonly string _apiKey;

        public SendGridEmailSender(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task Send(string htmlBody, EmailConfiguration emailConfiguration)
        {
            var client = new SendGridClient(_apiKey);

            // Send a Single Email using the Mail Helper with convenience methods and initialized SendGridMessage object
            var msg = new SendGridMessage()
            {
                From        = new EmailAddress(emailConfiguration.FromAddress, emailConfiguration.FromDisplayName),
                Subject     = emailConfiguration.Subject,
                HtmlContent = htmlBody
            };

            if(emailConfiguration.ToAddresses != null)
                foreach (var toAddress in emailConfiguration.ToAddresses)
                    msg.AddTo(new EmailAddress(toAddress));

            if (emailConfiguration.CcAddresses != null)
                foreach (var ccAddress in emailConfiguration.CcAddresses)
                    msg.AddCc(new EmailAddress(ccAddress));

            if (emailConfiguration.BccAddresses != null)
                foreach (var bccAddress in emailConfiguration.BccAddresses)
                    msg.AddBcc(new EmailAddress(bccAddress));

            var response = await client.SendEmailAsync(msg);
        }
    }
}
