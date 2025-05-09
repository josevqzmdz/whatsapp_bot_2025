// ASP.NET version of "main" method in java or C/c++
// this is where the main program runs, gotta start small ya know
// basically sends the cURL/JSON thingy to my main phone using whatsapp API
// and thats it, but running through ASP.NET as opposed to sending it 
// through POSTMAN with a JSON file

using System.Text.Json;
using whatsapp_tests.MongoDB_Boilerplate;
using whatsapp_tests.Services.Server;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

///////////////////////////////////////////////

builder.Services.AddHttpClient<WhatsAppService>();
// builds the main instance
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Add services to the container.

// run mongodb ping
// var mongo = new MongoDBConnection();
// mongo.Ping();

// createss the whatsapp webhook controller
app.MapPost("/webhook", async (HttpRequest request, WhatsAppService whatsappservicemainmenucfe) =>
{
    using var reader = new StreamReader(request.Body);
    var body = await reader.ReadToEndAsync();
    

    // TODO: find a way to change the value of the phone (my phone in this case)
    // for oncoming bot requests
    var phone = "523541090470";

    //var payload = JsonSerializer.Deserialize<WhatsAppService>(body);
    //var userPhone = payload?.entry?[0]?.changes?[0]?.value?.messages?[0]?.from;

    await whatsappservicemainmenucfe.SendMainMenuAsync(phone);

    // the following code reads the reply from the user and 
    // makes a choice based on his reply

    Console.WriteLine("Received message: " + body);

    return Results.Ok();
});

app.Run();

