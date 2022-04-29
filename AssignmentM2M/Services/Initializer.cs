using AssignmentM2M.Models.Entities;

namespace AssignmentM2M.Services;

/// <summary>
/// 
/// </summary>
public class Initializer
{
    /// <summary>
    /// The database
    /// </summary>
    private readonly ApplicationDbContext _db;

    /// <summary>
    /// Initializes a new instance of the <see cref="Initializer"/> class.
    /// </summary>
    /// <param name="db">The database.</param>
    public Initializer(ApplicationDbContext db)
    {
        _db = db;
    }

    /// <summary>
    /// Seeds the database.
    /// </summary>
    public void SeedDatabase()
    {
        _db.Database.EnsureCreated();

        // If there are any students then assume the database is already
        // seeded.
        if (_db.Stores.Any()) return;

        var stores = new List<Store>
        {
           new Store
              { StoreCode="001", Name="Walmart", Address="105 Wallway Rd"},
        };

        _db.Stores.AddRange(stores);
        _db.SaveChanges();

        var suppliers = new List<Supplier>
        {
           new Supplier { SupplierCode="001", Name ="Dell"},

        };

        _db.Suppliers.AddRange(suppliers);
        _db.SaveChanges();


        var items = new List<Item>
        {
           new Item { Name="DesktopComputer01"},
           new Item { Name="DesktopComputer02"}

        };

        _db.Items.AddRange(items);
        _db.SaveChanges();


        var storeitems = new List<StoreItem>
        {
           new StoreItem { Name="DesktopComputer01", ItemId=1, StoreId="001", Price=400.99},

        };

        _db.StoreItems.AddRange(storeitems);
        _db.SaveChanges();

        var supplieritems = new List<SupplierItem>
        {
           new SupplierItem { Name="DesktopComputer01", Price=200.99, ItemId=1, SupplierId="001"},
        };
        _db.SupplierItems.AddRange(supplieritems);
        _db.SaveChanges();
    }
}


