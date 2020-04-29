using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlinq.WebHook.Client.Configuration
{
    public class EmailConfiguration
    {
        public IEnumerable<string> ToAddresses { get; set; }
        public IEnumerable<string> BccAddresses { get; set; }
        public IEnumerable<string> CcAddresses { get; set; }
        public string FromAddress { get; set; }
        public string FromDisplayName { get; set; }
        public string Subject { get; set; }
    }
}
