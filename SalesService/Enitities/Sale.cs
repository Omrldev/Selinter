namespace SalesService.Enitities
{
    public class Sale
    {
        public Guid Id { get; set; }
        public int Price { get; set; }
        public int ReservePrice { get; set; }
        public int SoldPrice { get; set; }
        public string Seller { get; set; }
        public string Buyer { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Updated { get; set; } = DateTime.UtcNow;
        public Status Status { get; set; }
        public Product Product { get; set; }

    }
}
