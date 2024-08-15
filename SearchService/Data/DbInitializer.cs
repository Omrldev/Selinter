using MongoDB.Driver;
using MongoDB.Entities;
using RabbitMQ.Client;
using SearchService.Dto;
using System.Text.Json;

namespace SearchService.Data
{
    public class DbInitializer
    {
        public static async Task InitDb(WebApplication app)
        {
            var coName = "MongoDbConnection";
            var connection = app.Configuration.GetConnectionString(coName);
            await DB.InitAsync("SearchDb", MongoClientSettings.FromConnectionString(connection));

            await DB.Index<Product>()
                .Key(x => x.Brand, KeyType.Text)
                .Key(x => x.Category, KeyType.Text)
                .Key(x => x.Quality, KeyType.Text)
                .CreateAsync();

            // check if we have products into our database
            var count = await DB.CountAsync<Product>();

            if (count == 0) 
            {
                Console.WriteLine("no data seeded");

                var productData = await File.ReadAllTextAsync("Data/Sale.json");

                var options = new JsonSerializerOptions 
                { 
                    PropertyNameCaseInsensitive = true
                };

                var product = JsonSerializer
                    .Deserialize<List<Product>>(productData, options);

                await DB.SaveAsync(product);
            }

        }
    }
}
