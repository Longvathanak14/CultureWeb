using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace CultureWeb.Models
{
    public class ProductImage
    {
        public int Id { get; set; }
        public List<string> ImageUrls { get; set; } // Store the URLs or file paths of images

        // Property to represent the related product
        public int ProductId { get; set; }

        // Navigation property for the related product
        [ForeignKey("ProductId")]
        public virtual Products Product { get; set; }
    }
}
