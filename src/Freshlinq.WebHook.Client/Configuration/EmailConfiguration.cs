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
