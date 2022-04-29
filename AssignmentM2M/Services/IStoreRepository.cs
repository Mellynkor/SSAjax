using AssignmentM2M.Models.Entities;

namespace AssignmentM2M.Services
{

    /// <summary>
    /// Interface for Store Repo
    /// </summary>
    public interface IStoreRepository
    {
        /// <summary>
        /// Creates the specified new store.
        /// </summary>
        /// <param name="newStore">The new store.</param>
        /// <returns></returns>
        Store Create(Store newStore);

        /// <summary>
        /// Reads in a store based on integer ID
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        Store? Read(string code);

        /// <summary>
        /// Reads in all Stores
        /// </summary>
        /// <returns></returns>
        ICollection<Store> ReadAll();

        /// <summary>
        /// Updates the specified code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="store">The store.</param>
        void Update(string code, Store store);

        /// <summary>
        /// Deletes the specified code.
        /// </summary>
        /// <param name="code">The code.</param>
        void Delete(string code);
        
    }
}
