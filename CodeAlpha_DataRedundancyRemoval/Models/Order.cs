using System.ComponentModel.DataAnnotations;

namespace CodeAlpha_DataRedundancyRemoval.Models
{
    /// <summary>
    /// An Order references a single Customer via CustomerId (foreign key).
    /// The many products on an order are captured through the OrderDetail
    /// junction table instead of duplicating product/customer data per line.
    /// </summary>
    public class Order
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Order Date")]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

        public Customer? Customer { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        [Display(Name = "Order Total")]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public decimal Total => OrderDetails?.Sum(od => od.Quantity * od.UnitPrice) ?? 0;
    }
}
