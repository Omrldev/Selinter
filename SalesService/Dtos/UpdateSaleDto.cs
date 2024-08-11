namespace SalesService.Dtos
{
    public class UpdateSaleDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public string Quality { get; set; }
        public string Image { get; set; }
        public int? Price { get; set; }
    }
}
