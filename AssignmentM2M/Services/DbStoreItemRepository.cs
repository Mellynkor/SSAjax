using AssignmentM2M.Models;
using AssignmentM2M.Models.Entities;
using AssignmentM2M.Services;
using Microsoft.EntityFrameworkCore;

namespace AssignmentM2M.Services
{
    /// <summary>
    /// Item Repository that stores operation methods for items
    /// </summary>
    /// <seealso cref="AssignmentM2M.Services.IStoreItemRepository" />
    public class DbStoreItemRepository : IStoreItemRepository
    {
        /// <summary>
        /// The database
        /// </summary>
        private readonly ApplicationDbContext _db;
        /// <summary>
        /// The store repo
        /// </summary>
        private readonly IStoreRepository _storeRepo;
        /// <summary>
        /// The item repo
        /// </summary>
        private readonly IItemRepository _itemRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbStoreItemRepository"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        /// <param name="storeRepo">The store repo.</param>
        /// <param name="itemRepo">The item repo.</param>
        public DbStoreItemRepository(ApplicationDbContext db,
            IStoreRepository storeRepo,
            IItemRepository itemRepo)
        {
            _db = db;
            _storeRepo = storeRepo;
            _itemRepo = itemRepo;
        }

        /// <summary>
        /// Creates the specified code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="itemId">The item identifier.</param>
        /// <returns></returns>
        public StoreItem? Create(string code, int itemId)
        {
            var store = _storeRepo.Read(code); //get the store
            if (store == null)
            {
                return null;
            }
            var storeItem = store.StoreItems //get the items already registered to the store
                .FirstOrDefault(si => si.ItemId == itemId);
            if (storeItem != null)
            {
                return null;
            }
            var item = _itemRepo.Read(itemId); //read in the specific item
            if (item == null)
            {
                return null;
            }
            var newStoreItem = new StoreItem //create a new store item that is linked to the selected store and item
            {
                Name = item.Name,
                Store = store,
                Item = item,
                StoreId = code,
                ItemId = itemId,
            };
            store.StoreItems.Add(newStoreItem); //Add it to the store's items
           // item.ItemSellers.Add(store);
            _db.SaveChanges(); //save changes
            return newStoreItem;
        }


        /// <summary>
        /// Read an Item entity based on ID
        /// </summary>
        /// <param name="id">The ID of the Item to read</param>
        /// <returns></returns>
        public StoreItem? Read(int id)
        {
            return _db.StoreItems
                .Include(si => si.Store)
                .Include(si => si.Item)
                .FirstOrDefault(si => si.Id == id);
        }

        /// <summary>
        /// Read ALL Item entities
        /// </summary>
        /// <returns></returns>
        public ICollection<StoreItem> ReadAll()
        {
            return _db.StoreItems
                .Include(si => si.Store)
                .Include(si => si.Item)
                .ToList();

        }

        /// <summary>
        /// Updates the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="price">The price.</param>
        public void Update(int id, double price)
        {
            StoreItem? itemToUpdate = Read(id);
            if (itemToUpdate != null)
            {
                itemToUpdate.Price = price;
                _db.SaveChanges();
            }
        }

        /*
        public void Remove(string storecode, int id)
        {
            StoreItem? itemToDelete = Read(id);
            if (itemToDelete != null)
            {
                _db.StoreItems.Remove(itemToDelete);
                _db.SaveChanges();
            }

        }
        */
        /*
        /// <summary>
        /// Removes the specified storecode.
        /// </summary>
        /// <param name="storecode">The storecode.</param>
        /// <param name="id">The identifier.</param>
        public void Remove(string storecode, int id)
        {
            var store = _storeRepo.Read(storecode);
            var storeItem = store!.StoreItems
                .FirstOrDefault(si => si.Id == id);
            store!.StoreItems.Remove(storeItem);
            _db.SaveChanges();
        }
        */

        /// <summary>
        /// Deletes the specified suppliercode.
        /// </summary>
        /// <param name="suppliercode">The suppliercode.</param>
        /// <param name="itemId">The item identifier.</param>
        public void Remove(string storecode, int id)
        {
            var store = _storeRepo.Read(storecode);
            StoreItem? itemToDelete = Read(id);
            if (itemToDelete != null)
            {
                store.StoreItems.Remove(itemToDelete);
                _db.StoreItems.Remove(itemToDelete);
                _db.SaveChanges();
            }

        }

    }
}
