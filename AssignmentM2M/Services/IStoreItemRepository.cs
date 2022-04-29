using AssignmentM2M.Models.Entities;

namespace AssignmentM2M.Services
{

    /// <summary>
    /// Interface for Items Repo
    /// </summary>
    public interface IStoreItemRepository
    {
        /// <summary>
        /// Creates the specified code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="itemId">The item identifier.</param>
        /// <returns></returns>
        StoreItem? Create(string code, int itemId);

        /// <summary>
        /// Reads in an item based on integer ID
        /// </summary>
        /// <param name="id">ID of item to be read in</param>
        /// <returns></returns>
        StoreItem? Read(int id);

        /// <summary>
        /// Reads in all Items
        /// </summary>
        /// <returns></returns>
        ICollection<StoreItem> ReadAll();

        /// <summary>
        /// Updates the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="price">The price.</param>
        void Update(int id, double price);

        /// <summary>
        /// Removes the specified storecode.
        /// </summary>
        /// <param name="storecode">The storecode.</param>
        /// <param name="id">The identifier.</param>
        void Remove(string storecode, int id);
        
    }
}
