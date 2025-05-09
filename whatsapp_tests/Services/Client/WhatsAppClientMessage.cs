namespace whatsapp_tests.Services.Client
{
    // docs: https://developers.facebook.com/docs/whatsapp/cloud-api/guides/set-up-webhooks
    // this method's purpose is to serialize the JSON data dump of
    // whoever is trying to reach our bot API's and be able to
    // parse it from one endpoint to another as seamlessly as possible
    public class WhatsAppClientMessage
    {
        public WhatsAppClientMessage(
            String whatsapp_acc, // also called "object" in the cURL JSON
            String entry,
            String id,
            String changes,
            String value,
            String messaging_product,
            String metadata,
            String display_phone_number,
            String phone_number_id,
            String field
            )
        {
            var jsonBody = new
            {

            };// end of jsonBody
        }
    }


}
