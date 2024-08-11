using System.ComponentModel.DataAnnotations;

namespace SalesService.Dtos
{
    public class CreateSaleDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Quality { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public int Price { get; set; }
        public DateTime Created { get; set; }
    }
}
