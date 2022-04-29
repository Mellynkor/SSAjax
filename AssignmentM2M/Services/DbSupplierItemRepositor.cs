using AssignmentM2M.Models;
using AssignmentM2M.Models.Entities;
using AssignmentM2M.Services;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AssignmentM2M.Services
{
    /// <summary>
    /// Item Repository that stores operation methods for items
    /// </summary>
    /// <seealso cref="AssignmentM2M.Services.ISupplierItemRepository" />
    public class DbSupplierItemRepository : ISupplierItemRepository
    {

        /// <summary>
        /// The database
        /// </summary>
        private readonly ApplicationDbContext _db;
        /// <summary>
        /// The supplier repo
        /// </summary>
        private readonly ISupplierRepository _supplierRepo;
        /// <summary>
        /// The item repo
        /// </summary>
        private readonly IItemRepository _itemRepo;


        /// <summary>
        /// Initializes a new instance of the <see cref="DbSupplierItemRepository"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        /// <param name="supplierRepo">The supplier repo.</param>
        /// <param name="itemRepository">The item repository.</param>
        public DbSupplierItemRepository(
            ApplicationDbContext db,
            ISupplierRepository supplierRepo, IItemRepository itemRepository)
        {
            _db = db;
            _supplierRepo = supplierRepo;
            _itemRepo = itemRepository;
        }


        /// <summary>
        /// Reads in an item based on integer ID
        /// </summary>
        /// <param name="id">ID of item to be read in</param>
        /// <returns></returns>
        public SupplierItem? Read(int id)
        {
            return _db.SupplierItems
             .Include(supi => supi.Supplier)
             .Include(supi => supi.Item)
             .FirstOrDefault(supi => supi.Id == id);

        }


        /// <summary>
        /// Reads in all Items
        /// </summary>
        /// <returns></returns>
        public ICollection<SupplierItem> ReadAll()
        {
            return _db.SupplierItems
               .Include(supi => supi.Supplier)
               .Include(supi => supi.Item)
               .ToList();
        }


        /// <summary>
        /// Creates the specified code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="itemId">The item identifier.</param>
        /// <returns></returns>
        public SupplierItem? Create(string code, int itemId)
        {
            var supplier = _supplierRepo.Read(code); //Read in the supplier
            if (supplier == null)
            {
                Debug.WriteLine(supplier.ToString() + "N1");
                return null;
            }
            Debug.WriteLine(supplier.ToString() + "HEREEEEEEEEEEEEEE");
            var supplierItem = supplier.SupplierItems //Read to see if the item is already registered
                .FirstOrDefault(si => si.ItemId == itemId);
            if (supplierItem != null)
            {
                Debug.WriteLine(supplierItem.ToString() + "NOT NULL2");
                return null;
            }
            //Debug.WriteLine(supplierItem.ToString());
            var item = _itemRepo.Read(itemId); //read the item to be added
            if (item == null)
            {
                Debug.WriteLine(item.ToString() + "NULL 3");
                return null;
            }
            Debug.WriteLine(item.ToString() + "HEREEEEEEEEEEEEEE");
            var newSupplierItem = new SupplierItem //create a new supplier item
            {
                Name = item.Name,
                Supplier = supplier,
                Item = item,
                SupplierId = code,
                ItemId = itemId,
            };
            Debug.WriteLine(newSupplierItem.Name.ToString() + "HEREEEEEEEEEEEEEE");
            supplier.SupplierItems.Add(newSupplierItem);
            _db.SaveChanges();
            Debug.WriteLine(supplier.SupplierItems.Count().ToString() + "COUNT");
            Debug.WriteLine(supplier.SupplierItems.ToString() + "HEREEEEEEEEEEEEEE");
            return newSupplierItem;
        }

        /// <summary>
        /// Updates the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="price">The price.</param>
        public void Update(int id, double price)
        {
            SupplierItem? itemToUpdate = Read(id);
            if (itemToUpdate != null)
            {
                itemToUpdate.Price = price;
                _db.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes the specified suppliercode.
        /// </summary>
        /// <param name="suppliercode">The suppliercode.</param>
        /// <param name="itemId">The item identifier.</param>
        public void Delete(string suppliercode, int itemId)
        {
            var supplier = _supplierRepo.Read(suppliercode);
            SupplierItem? itemToDelete = Read(itemId);
            if (itemToDelete != null)
            {
                supplier.SupplierItems.Remove(itemToDelete);
                _db.SupplierItems.Remove(itemToDelete);
                _db.SaveChanges();
            }

        }

    }
}
