using System.ComponentModel.DataAnnotations;

namespace CodeAlpha_DataRedundancyRemoval.Models
{
    /// <summary>
    /// Customer information is stored once and referenced by Orders via a
    /// foreign key, instead of repeating the customer's name/email/address
    /// on every order row.
    /// </summary>
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; } = string.Empty;

        [Phone]
        [StringLength(20)]
        public string? Phone { get; set; }

        [StringLength(250)]
        public string? Address { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
