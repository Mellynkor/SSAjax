using AssignmentM2M.Models;
using AssignmentM2M.Models.Entities;
using AssignmentM2M.Services;
using Microsoft.EntityFrameworkCore;

namespace AssignmentM2M.Services
{
    /// <summary>
    /// Store Repository that stores operation methods for stores
    /// </summary>
    /// <seealso cref="AssignmentM2M.Services.IStoreRepository" />
    public class DbStoreRepository : IStoreRepository
    {
        /// <summary>
        /// The database
        /// </summary>
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbStoreRepository"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public DbStoreRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Creates the specified new store.
        /// </summary>
        /// <param name="newStore">The new store.</param>
        /// <returns></returns>
        public Store Create(Store newStore)
        {
            _db.Stores.Add(newStore);
            _db.SaveChanges();
            return newStore;
        }


        /// <summary>
        /// Reads in a store based on integer ID
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Store? Read(string code)
        {
            return _db.Stores
                .Include(s => s.StoreItems)
                    .ThenInclude(si => si.Item)
                .FirstOrDefault(s => s.StoreCode == code);
        }


        /// <summary>
        /// Reads in all Stores
        /// </summary>
        /// <returns></returns>
        public ICollection<Store> ReadAll()
        {
            return _db.Stores
                .Include(s => s.StoreItems)
                .ToList();
        }

        /// <summary>
        /// Updates the specified code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="store">The store.</param>
        public void Update(string code, Store store)
        {
            Store? storeToUpdate = Read(code);
            if (storeToUpdate != null)
            {
                storeToUpdate.Name = store.Name;
                storeToUpdate.Address = store.Address;
                _db.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes the specified code.
        /// </summary>
        /// <param name="code">The code.</param>
        public void Delete(string code)
        {
            Store? storeToDelete = Read(code);
            if (storeToDelete != null)
            {
                _db.Stores.Remove(storeToDelete);
                _db.SaveChanges();
            }

        }

    }
}
