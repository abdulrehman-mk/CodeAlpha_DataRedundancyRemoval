using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeAlpha_DataRedundancyRemoval.Models
{
    /// <summary>
    /// Junction table linking Orders and Products (many-to-many resolved as
    /// two one-to-many relationships). UnitPrice is captured at time of sale
    /// so historical orders remain accurate even if the product price later
    /// changes, without duplicating the full product record.
    /// </summary>
    public class OrderDetail
    {
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }
        public Order? Order { get; set; }

        [Required]
        [Display(Name = "Product")]
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }
    }
}
