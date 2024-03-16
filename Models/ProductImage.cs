using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace CultureWeb.Models
{
    public class ProductImage
    {
        public int Id { get; set; }
        public string? ImageUrl1 { get; set; } // Store the URL or file path of the first image
        public string? ImageUrl2 { get; set; } // Store the URL or file path of the second image
        public string? ImageUrl3 { get; set; } // Store the URL or file path of the third image

        //[NotMapped] // Mark the property as not mapped to the database
        //public IFormFile ImageFile1 { get; set; }
        //[NotMapped] // Mark the property as not mapped to the database
        //public IFormFile ImageFile2 { get; set; }
        //[NotMapped] // Mark the property as not mapped to the database
        //public IFormFile ImageFile3 { get; set; }
        // Property to represent the related product
        public int ProductId { get; set; }

        // Navigation property for the related product
        //[ForeignKey("ProductId")]
        //public virtual Products Product { get; set; }
    }
}
