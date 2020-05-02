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
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Freshlinq.WebHook.Client
{
    public static class FreshlinqClient
    {
        [FunctionName("ExportSetWebHook")]
        public static async Task<IActionResult> RunExportSetWebHook(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log,
            ExecutionContext context)
        {
            log.LogInformation($"Freshlinq Export Set Web Hook - Started {DateTimeOffset.Now:O}");

            var config = GetConfiguration(context);

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var exportSetBody = JsonConvert.DeserializeObject<ExportSetWebHookBody>(requestBody);

            if (exportSetBody == null)
            {
                log.LogError("Web Hook call failed, could not process event body");
                return new BadRequestResult();
            }

            if (config.Email != null)
            {
                var body = new EmailBody(exportSetBody);
                var bodyHtml = body.Build();

                if (config.SendGrid != null && !string.IsNullOrEmpty(config.SendGrid.ApiKey))
                {
                    var sender = new SendGridEmailSender(config.SendGrid.ApiKey);
                    await sender.Send(bodyHtml, config.Email);
                }
            }

            string responseMessage = $"Web Hook call for file '{exportSetBody.FileName}'. This HTTP triggered function executed successfully.";

            log.LogInformation(responseMessage);

            return new OkResult();
        }


        private static Configuration.Configuration GetConfiguration(ExecutionContext context)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(context.FunctionAppDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("clientConfiguration.json", optional: false, reloadOnChange: true)
                .AddJsonFile("local.clientConfiguration.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();


            var settings = new Configuration.Configuration();
            config.Bind(settings);

            return settings;
        }
    }
}
