using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssignmentM2M.Models.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class StoreItem
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        public double Price { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the store identifier.
        /// </summary>
        /// <value>
        /// The store identifier.
        /// </value>
        [ForeignKey("StoreCode")]
        public string StoreId { get; set; } = String.Empty;
        /// <summary>
        /// Gets or sets the store.
        /// </summary>
        /// <value>
        /// The store.
        /// </value>
        public Store? Store { get; set; }

        /// <summary>
        /// Gets or sets the item identifier.
        /// </summary>
        /// <value>
        /// The item identifier.
        /// </value>
        [ForeignKey("ItemId")]
        public int ItemId { get; set; }
        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        /// <value>
        /// The item.
        /// </value>
        public Item? Item { get; set; }

    }

}
