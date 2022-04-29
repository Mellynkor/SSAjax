using System.ComponentModel.DataAnnotations;

namespace AssignmentM2M.Models.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class Supplier
    {
        /// <summary>
        /// Gets or sets the supplier code.
        /// </summary>
        /// <value>
        /// The supplier code.
        /// </value>
        [Key, StringLength(3)]
        public string SupplierCode { get; set; } = String.Empty;
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [StringLength(20)]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [StringLength(50)]
        public string? Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the supplier items.
        /// </summary>
        /// <value>
        /// The supplier items.
        /// </value>
        public ICollection<SupplierItem> SupplierItems { get; set; } = new List<SupplierItem>();
    }
}
