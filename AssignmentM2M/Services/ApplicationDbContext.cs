using AssignmentM2M.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AssignmentM2M.Services;

/// <summary>
/// 
/// </summary>
/// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
public class ApplicationDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
    /// </summary>
    /// <param name="options">The options for this context.</param>
    /// <remarks>
    /// See <see href="https://aka.ms/efcore-docs-dbcontext">DbContext lifetime, configuration, and initialization</see> and
    /// <see href="https://aka.ms/efcore-docs-dbcontext-options">Using DbContextOptions</see> for more information.
    /// </remarks>
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    /// <summary>
    /// Gets the stores.
    /// </summary>
    /// <value>
    /// The stores.
    /// </value>
    public DbSet<Store> Stores => Set<Store>();
    /// <summary>
    /// Gets the suppliers.
    /// </summary>
    /// <value>
    /// The suppliers.
    /// </value>
    public DbSet<Supplier> Suppliers => Set<Supplier>();
    /// <summary>
    /// Gets the items.
    /// </summary>
    /// <value>
    /// The items.
    /// </value>
    public DbSet<Item> Items => Set<Item>();
    /// <summary>
    /// Gets the store items.
    /// </summary>
    /// <value>
    /// The store items.
    /// </value>
    public DbSet<StoreItem> StoreItems => Set<StoreItem>();

    /// <summary>
    /// Gets the supplier items.
    /// </summary>
    /// <value>
    /// The supplier items.
    /// </value>
    public DbSet<SupplierItem> SupplierItems => Set<SupplierItem>();
}


