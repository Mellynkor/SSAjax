using AssignmentM2M.Models.Entities;

namespace AssignmentM2M.Services
{

    /// <summary>
    /// Interface for Items Repo
    /// </summary>
    public interface ISupplierRepository
    {
        /// <summary>
        /// Creates the specified new supplier.
        /// </summary>
        /// <param name="newSupplier">The new supplier.</param>
        /// <returns></returns>
        Supplier Create(Supplier newSupplier);

        /// <summary>
        /// Reads in an item based on integer ID
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        Supplier? Read(string code);

        /// <summary>
        /// Reads in all Items
        /// </summary>
        /// <returns></returns>
        ICollection<Supplier> ReadAll();

        /// <summary>
        /// Updates the specified code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="supplier">The supplier.</param>
        void Update(string code, Supplier supplier);

        /// <summary>
        /// Deletes the specified code.
        /// </summary>
        /// <param name="code">The code.</param>
        void Delete(string code);
        
    }
}
