using System.ComponentModel.DataAnnotations.Schema;

namespace SalesService.Enitities
{
    [Table("Products")]
    public class Product
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public string Quality { get; set; }
        public string Image { get; set; }

        // nav properties
        public Sale Sale { get; set; }
        public Guid SaleId { get; set; }

    }
}
