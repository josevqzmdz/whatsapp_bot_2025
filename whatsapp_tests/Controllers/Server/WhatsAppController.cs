using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace whatsapp_tests.Services.Server
{
    public class WhatsAppController
    {
        private readonly HttpClient _httpClient;

        // constructor
        /*
        public WhatsAppService(HttpClient httpClient, string WhatsAppToken)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    WhatsAppToken
            );
        }
        
        */

        // TO-DO: 
        // figure out a way to make the whatsapp token
        // not expire every hour
        // otherwise come here and generate a new one
        // https://developers.facebook.com/tools/explorer/
        public WhatsAppController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    "EAATyUT8goNsBO67mTHJs1wV1Epyj4RCoYoVjUZBpMacx2lr3Gz439POXIpEdRHd5wFZAa8mn67sPUlbpwV77xFakahfqk6yEZAEAV7oacAXZAWCcAucFEeFghFNPW7Ogyg8o5IDZBHb0tr5krUXkucFyj5MR6ZB9OPbAuSYDud64e1DahYZC7M59vjHeUbWIVmgZBTM0pZBGk5kxZCi6eLuZAnC5TliZBUQZD"
                );
        }

        /*
         * 
         *      ////////////////////////////////////////////////////////////////////////////////////////////////////////
         *      //////////////////////////////////////////////////////////////////////////////////////////////////////
         *      USER ACTIONS
         *      
         *      This entire section is dedicated to how the API consumes whatever info the 
         *      user sends and which sort of choices it takes
         *      
         *      /////////////////////////////////////////////////////////
         * */

        // simple method that sends a reply to the user making the request
        public async Task SendTextMessageAsync(string sendTo, string message)
        {
            // JSON body which our API sends to the user
            var jsonBody = new
            {
                messaging_product = "whatsap",
                to = sendTo,
                text = new { body = message },
                type = "text"
            };

            var content = new StringContent(JsonSerializer.Serialize(jsonBody), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://graph.facebook.com/v17.0/579992435207524/messages", content);
            response.EnsureSuccessStatusCode();
        }

        //  ################################################################
        //  ################################################################
        // #################################################################
        //                  GET 
        // simple method that gets the info being sent by the user
        public async Task<string> GetUserMessage(string userPhone)
        {
            try
            {
                string returnUserReply = "";
                HttpResponseMessage userReply = await _httpClient.GetAsync(userPhone);
                if (userReply.IsSuccessStatusCode)
                {
                    returnUserReply = await userReply.Content.ReadAsStringAsync();
                    return returnUserReply;
                }
                else
                {
                    return "received invalid info";
                }
                
            }
            catch(NullReferenceException ex)
            {
                Console.WriteLine("Error caught: NullReferenceException: ", ex.Message);
                return "error logged";
            }
            catch(ArgumentNullException ex)
            {
                Console.WriteLine("Error caught: ArgumentNullException: ", ex.Message);
                return "error logged";
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine("Error caught: ArgumentException: ", ex.Message);
                return "error logged";
            }
            catch(FormatException ex)
            {
                Console.WriteLine("Error caught: FormatException: ", ex.Message);
                return "error logged";
            }
            catch(InvalidOperationException ex)
            {
                Console.WriteLine("Error caught: InvalidOperationException: ", ex.Message);
                return "error logged";
            }
            
        }// end of GetUserMessage


        /*
         * 
         *      //////////////////////////////////////////////////////////////////////
         *      //////////////////////////////////////////////////////////////////////
         *      SERVER ACTIONS
         *      
         *      this entire section is dedicated to the info, choices, menus
         *      etc. that the app takes whenever the user initiates a sesion,
         *      makes a choice, etc.
         * 
         */

        // this method encapsulates the cURL Http request
        // generated by whatsapp's API 
        // 'toPhoneNumber' is the phone # of the client
        // who is requesting the bot's features
        // -------
        // POST
        // -------
        public async Task SendMainMenuAsync(string toPhoneNumber)
        {
            // this entire thing is what would go inside one of those
            // huge JSON packages that whatsapp API generates
            var jsonBody = new
            {
                messaging_product = "whatsapp",
                recipient_type = "individual",
                to = toPhoneNumber,
                type = "interactive",
                // this is the JSON block 
                // where the menu and its contents are displayed
                interactive = new
                {
                    type = "list",
                    // header
                    header = new
                    {
                        type = "text",
                        text = "Gracias por contactar a la CFE."
                    },
                    // body
                    body = new
                    {
                        text = "Haga click en en menu de opciones para elegir su consulta."
                    },
                    // action: options the whatsapp bot displays for the user
                    // to select with simple numerical commands
                    action = new
                    {
                        button = "Menu de opciones",
                        sections = new[]
                        {
                            new
                            {
                                title = "Menu principal",
                                rows = new[]
                                {
                                    new { 
                                        id = "ROW_CLEAR_BAL", 
                                        title = "1", 
                                        description = "Para Conocer saldo a favor, responder con 1." 
                                    },
                                    new { 
                                        id = "ROW_OUT_BAL", 
                                        title = "2", 
                                        description = "Para Conocer saldo a pagar, responder con 2." 
                                    },
                                    new { 
                                        id = "ROW_DUE_DATE", 
                                        title = "3", 
                                        description = "Para conocer fecha limite; responder al bot con 3."
                                    },
                                    new { 
                                        id = "ROW_SUPPORT", 
                                        title = "4", 
                                        description = "Si ninguna opcion satisface, contactar soporte." 
                                    }
                                }// end of rows
                            }// end of new JSON/cURL block
                        }//end of sections
                    }//end of action
                }// end of interactive block
            };
            
            // these two lines of code encapsulate the aforementioned...
            var content = new StringContent(JsonSerializer.Serialize(jsonBody), Encoding.UTF8, "application/json");
            // and then send an asyncronous request, to the graph.facebook webhook, then wait for a response
            var response = await _httpClient.PostAsync("https://graph.facebook.com/v17.0/579992435207524/messages", content);

            response.EnsureSuccessStatusCode();
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
        }// end of sendclearedbalance methodss

    }// end of whatsappserviceMenuCFE class
}
