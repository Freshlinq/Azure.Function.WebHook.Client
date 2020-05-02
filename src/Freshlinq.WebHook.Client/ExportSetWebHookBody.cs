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
