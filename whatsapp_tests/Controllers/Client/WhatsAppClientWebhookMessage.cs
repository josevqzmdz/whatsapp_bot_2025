using MongoDB.Driver;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using JsonConvert2 = Newtonsoft.Json.JsonConvert;
using System.Net.Http;
using whatsapp_tests.Services.Server;


namespace whatsapp_tests.Services.Client
{

    public class Value
    {
        public string? messaging_product { get; set; } = "whatsapp";
        public string? metadata { get; set; }
    }

    public class Changes
    {
        public string? field { get; set; }
        public Value? values { get; internal set; }
    }

    // docs: https://developers.facebook.com/docs/whatsapp/cloud-api/guides/set-up-webhooks
    // this method's purpose is to serialize the JSON data dump of
    // whoever is trying to reach our bot API's and be able to
    // parse it from one endpoint to another as seamlessly as possible
    public class WhatsAppClientWebhookMessage
    {
        // constructor
        public WhatsAppClientWebhookMessage(WhatsAppController whatsappController)
        {
            this._whatsappController = whatsappController;
        }
        // getters and setters
        public WhatsAppController? _whatsappController { get; set; }
        public string? whatsapp_acc { get; set; } // also called "object" in the cURL JSON
        public string? entry { get; set; }
        public string? id { get; set; }
        public string? changes { get; set; }
        public string? value { get; set; }
     
        public string? display_phone_number { get; set; }
        public string? phone_number_id { get; set; }
        public string? field { get; set; }

        public async Task getClientJson(string clientPhoneNumber)
        {
            // this entire thing is what would go inside one of those
            // huge JSON packages that whatsapp API generates

            var jsonBody = new
            {
                whatsapp_acc = "whatsapp_business_account",
                entry = new[]
                {
                    new
                    {
                        id = "WHATSAPP_BUSINESS_ACCOUNT_ID",
                        changes = new[]
                        {
			            // https://stackoverflow.com/questions/19535357/no-best-type-found-for-implicitly-typed-array
			            // this thing was giving the aforementioned error because, even if we are declaring
			            // anonymous types, they *need* to have the same definition
                            new
                            {
                                field = "messages",
                                value = new
                                {
                                    messaging_product = "whatsapp",
                                    metadata = JsonConvert2.SerializeObject(new
                                    {
                                        display_phone_number = clientPhoneNumber,
                                        phone_number_id = this.phone_number_id
                                    })
                                }
                            }
                        }// end of changes anon method
                    }
                }// end of entry anon method
            }; // end of jsonBody anon method

            // these two lines of code encapsulate the aforementioned...
            var content = new StringContent(JsonConvert2.SerializeObject(jsonBody), Encoding.UTF8, "application/json");
            // and then send an asyncronous request, to the graph.facebook webhook, then wait for a response
            var response = await _httpClient.PostAsync("https://graph.facebook.com/v17.0/579992435207524/messages", content);

            response.EnsureSuccessStatusCode();

        }// end of getClientJson method
    }   
}
