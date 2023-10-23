using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CultureWeb.Models
{
    public class ProductPrice
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product's Price is required.")]
        public decimal UnitPrice { get; set; }
        public decimal? Discount_Percent { get; set; }
        public decimal? Discount_Amount { get; set; }

        // Property to represent the related product
        public int ProductId { get; set; }

        // Navigation property for the related productd
        [ForeignKey("ProductId")]
        public virtual Products Products { get; set; }
    }
}
