using System;
using System.Collections.Generic;
using System.Text;

namespace Freshlinq.WebHook.Client
{
    public class ExportSetWebHookBody
    {
        public string Uri { get; set; }
        public DateTimeOffset UriValidUntil { get; set; }
        public string FileName { get; set; }
        public string ExportSetName { get; set; }
        public Guid CorrelationId { get; set; }
    }
}
