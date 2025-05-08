// ASP.NET version of "main" method in java or C/c++
// this is where the main program runs, gotta start small ya know
// basically sends the cURL/JSON thingy to my main phone using whatsapp API
// and thats it, but running through ASP.NET as opposed to sending it 
// through POSTMAN with a JSON file

using whatsapp_tests.MongoDB_Boilerplate;
using whatsapp_tests.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// run mongodb ping
var mongo = new MongoDBConnection();
mongo.Ping();

builder.Services.AddHttpClient<WhatsAppServiceMainMenuCFE>();
// builds the main instance
var app = builder.Build();
// createss the whatsapp webhook controller
app.MapPost("/webhook", async (HttpRequest request, WhatsAppServiceMainMenuCFE whatsappservicemainmenucfe) =>
{
    using var reader = new StreamReader(request.Body);
    var body = await reader.ReadToEndAsync();
    Console.WriteLine("Received message: " + body);

    // TODO: find a way to change the value of the phone (my phone in this case)
    // for oncoming bot requests
    var phone = "523541090470";
    await whatsappservicemainmenucfe.SendMainMenuAsync(phone);

    return Results.Ok();
});

app.Run();

/*
 * ignore this crap
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();

*/
