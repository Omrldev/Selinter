using MongoDB.Entities;

namespace SearchService.Dto
{
    public class Product : Entity
    {
        public int Price { get; set; }
        public int ReservePrice { get; set; }
        public int SoldPrice { get; set; }
        public string Seller { get; set; }
        public string Buyer { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string Status { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public string Quality { get; set; }
        public string Image { get; set; }
    }
}
