using MongoDB.Entities;
using SearchService.Dto;

namespace SearchService.Service
{
    public class SalesSvcHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public SalesSvcHttpClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<List<Product>> GetProductsForSearchDb()
        {
            var lastUpdated = await DB.Find<Product, string>()
                .Sort(x => x.Descending(a => a.Updated))
                .Project(x => x.Updated.ToString())
                .ExecuteFirstAsync();

            return await _httpClient.GetFromJsonAsync<List<Product>>
                (_config["SalesServiceUrl"] + "/api/sales?date=" + lastUpdated);
        }
    }
    
}
