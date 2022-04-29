using System.ComponentModel.DataAnnotations;

namespace AssignmentM2M.Models.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class Store
    {
        /// <summary>
        /// Gets or sets the store code.
        /// </summary>
        /// <value>
        /// The store code.
        /// </value>
        [Key, StringLength(3)]
        public string StoreCode { get; set; } = String.Empty;
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [StringLength(50)]
        public string Name { get; set; } = String.Empty;
        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public string Address { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the store items.
        /// </summary>
        /// <value>
        /// The store items.
        /// </value>
        public ICollection<StoreItem> StoreItems { get; set; } = new List<StoreItem>();

    }
}
