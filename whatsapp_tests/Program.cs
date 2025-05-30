﻿// ASP.NET version of "main" method in java or C/c++
// this is where the main program runs, gotta start small ya know
// basically sends the cURL/JSON thingy to my main phone using whatsapp API
// and thats it, but running through ASP.NET as opposed to sending it 
// through POSTMAN with a JSON file

using MongoDB.Bson;
using System.Text.Json;
using whatsapp_tests.MongoDB_Boilerplate;
using whatsapp_tests.Services.Client;
using whatsapp_tests.Services.Server;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

///////////////////////////////////////////////

builder.Services.AddHttpClient<WhatsAppController>();
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
// HttpClient is intended to be instantiated once per application, rather than per-use. See Remarks.
HttpClient client = new HttpClient();

WhatsAppController whatsappController = new WhatsAppController(
        client,
        // insert your own info here!
    );

// createss the whatsapp webhook controller
app.MapPost("/webhook", async (HttpRequest request, HttpResponse response) =>
{

    // reads the content of the message once
    using var reader = new StreamReader(request.Body);
    var body = await reader.ReadToEndAsync();

    // sends the main menu to the whatsapp phone number
    await whatsappController.SendMainMenuAsync();

    Console.WriteLine("message the user sent: ");
    
    WhatsAppClientWebhookMessage message = new WhatsAppClientWebhookMessage(whatsappController);
    await message.getClientJson();
    
    return Results.Ok();
});

app.Run();

