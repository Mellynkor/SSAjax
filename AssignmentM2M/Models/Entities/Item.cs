using System.ComponentModel.DataAnnotations;

namespace AssignmentM2M.Models.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class Item
    {

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [StringLength(50)]
        public string? Description { get; set; } = string.Empty;
        /*
        public ICollection<Store> ItemSellers { get; set; } = new List<Store>();

        public ICollection<Supplier> ItemSuppliers { get; set; } = new List<Supplier>();
        */
       
    }
}
