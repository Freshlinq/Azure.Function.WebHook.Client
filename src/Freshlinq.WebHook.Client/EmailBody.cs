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
using System.IO;
using System.Text;

namespace Freshlinq.WebHook.Client
{
    public class EmailBody
    {
        private string GetFilePath()  
        {
            if (File.Exists("./SampleEmailBody.html"))
                return "./SampleEmailBody.html";
            else if (File.Exists("../SampleEmailBody.html"))
                return "../SampleEmailBody.html";
            else if (File.Exists("./bin/SampleEmailBody.html"))
                return "./bin/SampleEmailBody.html";

            throw new FileNotFoundException("Could not find SampleEmailBody.html");
        }

        private string EmailBodyTemplate => File.ReadAllText(GetFilePath());

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
