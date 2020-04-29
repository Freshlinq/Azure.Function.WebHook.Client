using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Freshlinq.WebHook.Client
{
    public class EmailBody
    {
        private string EmailBodyTemplate => File.ReadAllText("./SampleEmailBody.html");

        private readonly ExportSetWebHookBody _webHookContent;
        public EmailBody(ExportSetWebHookBody webHookContent)
        {
            _webHookContent = webHookContent;
        }

        public string Build()
        {
            string result = EmailBodyTemplate
                .Replace("#ReceivedTimeStamp#", DateTimeOffset.Now.ToString("R"))
                .Replace("#ExportSetName#", _webHookContent.ExportSetName)
                .Replace("#FileName#", _webHookContent.FileName)
                .Replace("#UriValidUntil#", _webHookContent.UriValidUntil.ToString("R"))
                .Replace("#Uri#", _webHookContent.Uri)
                .Replace("#CorrelationId#", _webHookContent.CorrelationId.ToString("D"));

            return result;
        }
    }
}
