using AssignmentM2M.Models;
using AssignmentM2M.Models.Entities;
using AssignmentM2M.Services;
using Microsoft.EntityFrameworkCore;

namespace AssignmentM2M.Services
{
    /// <summary>
    /// Item Repository that stores operation methods for items
    /// </summary>
    /// <seealso cref="AssignmentM2M.Services.IItemRepository" />
    public class DbItemRepository : IItemRepository
    {
        /// <summary>
        /// The database
        /// </summary>
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbItemRepository"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public DbItemRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Creates the specified new item.
        /// </summary>
        /// <param name="newItem">The new item.</param>
        /// <returns></returns>
        public Item Create(Item newItem)
        {
            _db.Items.Add(newItem);
            _db.SaveChanges();
            return newItem;
        }


        /// <summary>
        /// Read an Item entity based on ID
        /// </summary>
        /// <param name="id">The ID of the Item to read</param>
        /// <returns></returns>
        public Item? Read(int id)
        {
            return _db.Items.Find(id);
        }

        /// <summary>
        /// Read ALL Item entities
        /// </summary>
        /// <returns></returns>
        public ICollection<Item> ReadAll()
        {
            return _db.Items.ToList();
        }

        /// <summary>
        /// Updates the specified old identifier.
        /// </summary>
        /// <param name="oldId">The old identifier.</param>
        /// <param name="item">The item.</param>
        public void Update(int oldId, Item item)
        {
            Item? itemToUpdate = Read(oldId);
            if (itemToUpdate != null)
            {
                itemToUpdate.Name = item.Name;
                itemToUpdate.Description = item.Description;
                _db.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
            Item? itemToDelete = Read(id);
            if (itemToDelete != null)
            {
                _db.Items.Remove(itemToDelete);
                _db.SaveChanges();
            }

        }

    }
}
