using AssignmentM2M.Models.Entities;

namespace AssignmentM2M.Services
{

    /// <summary>
    /// Interface for Items Repo
    /// </summary>
    public interface IItemRepository
    {
        /// <summary>
        /// Creates the specified new item.
        /// </summary>
        /// <param name="newItem">The new item.</param>
        /// <returns></returns>
        Item Create(Item newItem);

        /// <summary>
        /// Reads in an item based on integer ID
        /// </summary>
        /// <param name="id">ID of item to be read in</param>
        /// <returns></returns>
        Item? Read(int id);

        /// <summary>
        /// Reads in all Items
        /// </summary>
        /// <returns></returns>
        ICollection<Item> ReadAll();

        /// <summary>
        /// Updates the specified old identifier.
        /// </summary>
        /// <param name="oldId">The old identifier.</param>
        /// <param name="item">The item.</param>
        void Update(int oldId, Item item);

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void Delete(int id);
        
    }
}
