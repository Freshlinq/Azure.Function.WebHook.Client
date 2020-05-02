# Freshlinq.Webhook

A sample web hook client in an Azure Function app. This is a good place to start when implementing a web hook service to handle calls from the Freshlinq trading platform.

This implementation includes a simple SendGrid mail sender which will send out notification emails to configured destinations.

Please update the ```clientConfiguration.json``` configuration file with SendGrid and email settings:
````
{
  "sendGrid": {
    "apiKey": "<Your SendGrid API Key>"
  },
  "email": {
    "toAddresses": [
      "joe@contoso.com"
    ],
    "bccAddresses":  [
       "bcc@contoso.com"
    ],
    "ccAddresses":  [],
    "fromAddress": "webhook@contoso.com",
    "fromDisplayName": "Contoso Web Hook Function", 
    "subject":  "New Web Hook Call From Freshlinq"
  } 
}
````

Please reach out to us should you need any help with your Freshlinq trading platform integration.
