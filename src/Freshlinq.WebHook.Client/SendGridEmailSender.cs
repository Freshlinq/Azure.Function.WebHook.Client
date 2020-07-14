/*
This file is part of Freshlinq.WebHook.

Freshlinq.WebHook is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

Freshlinq.WebHook is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with Freshlinq.WebHook.  If not, see <https://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Freshlinq.WebHook.Client.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Freshlinq.WebHook.Client
{
    public class SendGridEmailSender
    {
        private readonly string _apiKey;
        private readonly ILogger _logger;

        public SendGridEmailSender(string apiKey, ILogger logger)
        {
            _apiKey = apiKey;
            _logger = logger;
        }

        public async Task Send(string htmlBody, string subjectLine, string webHookBody, EmailConfiguration emailConfiguration)
        {
            var messageBodyAttachment = new Attachment();
            messageBodyAttachment.Disposition = "attachment";
            messageBodyAttachment.Filename = "webhookBody.json";
            messageBodyAttachment.Type = "application/json";
            messageBodyAttachment.Content = Convert.ToBase64String(Encoding.UTF8.GetBytes(webHookBody));

            var attachments = new List<Attachment>();
            attachments.Add(messageBodyAttachment);

            var client = new SendGridClient(_apiKey);

            // Send a Single Email using the Mail Helper with convenience methods and initialized SendGridMessage object
            var msg = new SendGridMessage()
            {
                From        = new EmailAddress(emailConfiguration.FromAddress, emailConfiguration.FromDisplayName),
                Subject     = subjectLine,
                HtmlContent = htmlBody,
                Attachments = attachments
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

            _logger.LogInformation("Message handed over to SendGrid with response {sendGridResponseCode} - {sendGridResponseBody}", response.StatusCode, response.Body.ReadAsStringAsync().Result);
        }
    }
}
