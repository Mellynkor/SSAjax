using AssignmentM2M.Models.Entities;

namespace AssignmentM2M.Models.Entities.ViewModels;

/// <summary>
/// 
/// </summary>
public class SupplierItemVM
    {
    /// <summary>
    /// Gets or sets the supplier.
    /// </summary>
    /// <value>
    /// The supplier.
    /// </value>
    public Supplier? Supplier { get; set; }
    /// <summary>
    /// Gets or sets the item.
    /// </summary>
    /// <value>
    /// The item.
    /// </value>
    public Item? Item { get; set; }

    }

