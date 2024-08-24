using Amazon.Runtime.Internal;
using MongoDB.Driver;
using MongoDB.Entities;
using RabbitMQ.Client;
using SearchService.Dto;
using SearchService.Service;
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

            using var scope = app.Services.CreateScope();

            var httpClient = scope.ServiceProvider.GetRequiredService<SalesSvcHttpClient>();

            var products = await httpClient.GetProductsForSearchDb();

            Console.WriteLine(products.Count + " products returned from our sales service");

            if (products.Count > 0)
            {
                await DB.SaveAsync(products);
            }

            /*if (count == 0) 
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
            }*/

        }
    }
}
