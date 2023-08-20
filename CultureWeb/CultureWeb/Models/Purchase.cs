using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CultureWeb.Models
{
    public class Purchase
    {
        public int Id { get; set; }

        [Display(Name = "Purchase No")]
        public string PurchaseNo { get; set; }
        public string UserId { get; set; } // User's identifier who purchased the product
        public virtual ApplicationUser User { get; set; }
        public int SupplierId { get; set; }

        [ForeignKey("SupplierId")]
        public virtual Supplier? Suppliers { get; set; } // Navigation property
        public DateTime PurchaseDate { get; set; }
        public virtual List<PurchaseDetail> PurchaseDetails { get; set; }
    }
}
