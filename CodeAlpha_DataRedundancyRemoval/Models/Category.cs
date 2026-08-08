using System.ComponentModel.DataAnnotations;

namespace CodeAlpha_DataRedundancyRemoval.Models
{
    /// <summary>
    /// Categories are stored once and referenced by Products via a foreign key,
    /// instead of repeating the category name/description on every product row.
    /// This removes redundant, duplicated category data (normalization).
    /// </summary>
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(100)]
        [Display(Name = "Category Name")]
        public string Name { get; set; } = string.Empty;

        [StringLength(300)]
        public string? Description { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
