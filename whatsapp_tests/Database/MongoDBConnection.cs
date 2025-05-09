// boilerplate para crear una conexion entre mongoDB y ASP.NET
// para obtener la info general del usuario de whatsapp

using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver;
using System;

namespace whatsapp_tests.MongoDB_Boilerplate
{
    public class MongoDBConnection
    {
        private const string connectionUri = "mongodb+srv://josevqzmdz:YgtU7lc9YCBKTz8Y@primercliente.kn7tflx.mongodb.net/?retryWrites=true&w=majority&appName=primercliente";
        private readonly MongoClient client;

        public MongoDBConnection()
        {
            var settings = MongoClientSettings.FromConnectionString(connectionUri);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            client = new MongoClient(settings);
        }

        // simple ping para revisar conexion
        public void Ping()
        {
            try
            {
                var result = client.GetDatabase("admin").RunCommand<BsonDocument>(new BsonDocument("ping", 1));
                Console.WriteLine("Ping exitoso!");
            } 
            catch (Exception ex)
            {
                Console.WriteLine($"conexion fracasada: {ex.Message}");
            }
        }
    }
}