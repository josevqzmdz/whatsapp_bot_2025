namespace whatsapp_tests.Services
{
    // this JSON/cURL menu displays whenever the client
    // selects '1' in the WhatsAppServiceMainMenuCFE list of options
    // "mostrar saldo a favor" something to that effect
    public class WhatsAppServiceClearedBalanceCFE
    {
        private readonly HttpClient _httpClient;

        // constructors for whenever you have a whatsapp token or not
        public WhatsAppServiceClearedBalanceCFE(HttpClient httpClient, String WhatsAppToken)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue(
                        "Bearer",
                        WhatsAppToken
                    );
        }

        public WhatsAppServiceClearedBalanceCFE(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue(
                        "Bearer",
                        "EAATyUT8goNsBO78PyQq3tlsaWnzbTixRjgmt3BXMTyLjMMbj2TZCuMLBaO6IcACTg378MBthV6S1dBDWIUd66nY2nVq3ZBexXWGUGYLWBqh7h7T9UZAlSj5BveosOVi7vv02YiaBQReeh5IckMazdPR4f85ZCOzXcEkmCZB64ozUgFcOr5TRvdZCKJ1etF1kQpVqiHoLopqXKBLBjZCjNZBtiWe9ebqxMeR9crJVRR3zYbQZD"
                    );
        }

        // this method encapsulates the entire JSON/cURL info that gets
        // retrieved from whatever database this mock bot is supposed to
        // get its info from, in this case
        // the CFE, comision fed de electricidad, from mexico
        // here we suppose the client can actually get his
        // cleared balance from the bot
        public async Task SendClearedBalanceAsync(string toPhoneNumber)
        {
            var jsonBody = new
            {
                messaging_product = "whatsapp",
                recipient_type = "individual",
                to = toPhoneNumber,
                type = "interactive",
                interactive = new
                {
                    type = "list",
                    header = new
                    {
                        type = "text",
                        text = "Opcion 1: Mostrar saldo a favor."
                    },
                    body = new
                    {
                        text = "Usted tiene un saldo a favor de $XX.XX ."
                    },
                    action = new
                    {
                        button = "Menu de opciones: ",
                        sections = new[]
                        {
                            new {
                                title = "saldo a favor: ",
                                // these rows showcase the only 2 options
                                // A) return to the previous menu
                                // B) dial costumer support

                                // Option A
                                // A) return to the previous menu
                                rows = new[]
                                {
                                    new {
                                        id = "ROW_RETURN_MENU",
                                        title = "A",
                                        description = "Para volver al menu anterior, escriba A."
                                    },
                                    // Option B
                                    // B) dial costumer support
                                    new {
                                        id = "ROW_REQ_SUPPORT",
                                        title = "B",
                                        description = "Para comunicarse con un ejecutivo, escriba B."
                                    }
                                }// end of options array
                            }// end of saldo a favor menu
                        }// end of section json array
                    }// end of action json array
                }//end of interactive json array
            };// end of  jsonbody object
        }// end of sendclearedbalance method
    }
}
