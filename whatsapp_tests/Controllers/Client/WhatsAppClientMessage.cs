namespace whatsapp_tests.Services.Client
{
    // docs: https://developers.facebook.com/docs/whatsapp/cloud-api/guides/set-up-webhooks
    // this method's purpose is to serialize the JSON data dump of
    // whoever is trying to reach our bot API's and be able to
    // parse it from one endpoint to another as seamlessly as possible
    public class WhatsAppClientMessage
    {
        public WhatsAppClientMessage(
            string whatsapp_acc, // also called "object" in the cURL JSON
            string entry,
            string id,
            string changes,
            string value,
            string messaging_product,
            string metadata,
            string display_phone_number,
            string phone_number_id,
            string field
            )
        {
            var jsonBody = new
            {

            };// end of jsonBody
        }

        // getters and setters
        public string whatsapp_acc { get; set; } // also called "object" in the cURL JSON
        public string entry { get; set; }
        public string id { get; set; }
        public string changes { get; set; }
        public string value { get; set; }
        public string messaging_product { get; set; }
        public string metadata { get; set; }
        public string display_phone_number { get; set; }
        public string phone_number_id { get; set; }
        public string field { get; set; }
    }


}
